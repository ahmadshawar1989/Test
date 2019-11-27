using CompanyName.ProjectName.Common.Exceptions;
using CompanyName.ProjectName.Common.Settings;
using CompanyName.ProjectName.Domain.Contracts;
using CompanyName.ProjectName.Domain.Contracts.Repositories;
using CompanyName.ProjectName.Domain.Contracts.Services;
using CompanyName.ProjectName.Integration;
using CompanyName.ProjectName.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CompanyName.ProjectName.Domain
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductLineRepository _productLineRepository;
        private readonly IUnitOfWork _unitOfWork;

        private static Dictionary<int, Thread> ThreadList = new Dictionary<int, Thread>();

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository, IProductOrderRepository productOrderRepository, IProductLineRepository productLineRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
            _productLineRepository = productLineRepository;
            _unitOfWork = unitOfWork;
        }

        public void MakeOrder(MakeOrder orderObj)
        {
            ProductOrder productOrder;
            List<ProductOrderItem> productOrderItems;

            // Add order record.
            var order = new Order
            {
                CustomerId = orderObj.CustomerId,
                CreatedOn = DateTime.UtcNow
            };

            _orderRepository.Add(order);

            // Add product order records.
            foreach (var op in orderObj.OrderedProducts)
            {
                productOrder = new ProductOrder
                {
                    Order = order,
                    ProductId = op.ProductId
                };

                _productOrderRepository.Add(productOrder);

                // Add product order item records based on the needed count.
                List<ProductLineItem> availableItems = _orderRepository.GetAvailableItems(op.ProductId, op.Count);
                productOrderItems = availableItems.Select(a => new ProductOrderItem
                {
                    ProductLineItemId = a.Id,
                    ProductOrder = productOrder
                }).ToList();

                _orderRepository.AddProductOrderItems(productOrderItems);
            }

            // Commit the changes to the database.
            _unitOfWork.Commit();
        }

        public IEnumerable<ProductLine> GetProductLines(int productId, ProductLineStatus status)
        {
            bool isRunning = status == ProductLineStatus.Running;
            return _productLineRepository.GetMany(a => a.ProductId == productId && a.IsRunning == isRunning);
        }

        public int GetAvailableItemsCount(int productId)
        {
            return _orderRepository.GetAvailableItemsCount(productId);
        }

        public void CheckProductsLines()
        {
            int availableCount;
            int amountToProduce;
            List<ProductLine> productLinesToStart;
            List<ProductLine> productLinesToStop;
            IEnumerable<ProductLine> productLines;
            IEnumerable<ProductLine> runningProductLines;

            // Get all products.
            IEnumerable<Product> products = _productRepository.GetAll();

            // Check the availability of items versus the minimum amount per product.
            foreach (var product in products)
            {
                availableCount = _orderRepository.GetAvailableItemsCount(product.Id);
                amountToProduce = product.MinAmount - availableCount;

                // If no amount to produce, then stop all running product lines.
                if (amountToProduce <= 0)
                {
                    runningProductLines = _productLineRepository.GetMany(a => a.ProductId == product.Id && a.IsRunning);
                    foreach (var pl in runningProductLines)
                    {
                        stopProductLine(pl);
                    }
                    continue;
                }

                // Need to produce items.
                // Determine the product lines to start.
                productLines = _productLineRepository.GetMany(a => a.ProductId == product.Id);

                // Already running product lines.
                runningProductLines = productLines.Where(a => a.IsRunning);

                productLinesToStart = new List<ProductLine>();
                productLinesToStop = runningProductLines.ToList();

                foreach (var line in runningProductLines)
                {
                    productLinesToStop.Remove(line);
                    amountToProduce -= line.ItemsPerHour;

                    if (amountToProduce <= 0)
                        break;
                }

                // If the running product lines are not enough, then specify new product lines to run.
                if (amountToProduce > 0)
                {
                    // Need more not running product lines to start.
                    foreach (var line in productLines.Where(a => !a.IsRunning))
                    {
                        productLinesToStart.Add(line);
                        amountToProduce -= line.ItemsPerHour;

                        if (amountToProduce <= 0)
                            break;
                    }
                }

                // Stop product lines if exist.
                foreach (var pl in productLinesToStop)
                {
                    stopProductLine(pl);
                }

                // Start product lines if exist.
                foreach (var pl in productLinesToStart)
                {
                    startProductLine(pl);
                }
            }

            // Commit the changes to the database.
            _unitOfWork.Commit();
        }

        private void startProductLine(ProductLine pl)
        {
            pl.IsRunning = true;
            _productLineRepository.Update(pl);

            Thread bgThread = new Thread(delegate ()
            {
                doLogic();
            });

            bgThread.Start();

            ThreadList.Add(pl.Id, bgThread);
        }

        private void stopProductLine(ProductLine pl)
        {
            pl.IsRunning = false;
            _productLineRepository.Update(pl);

            if (ThreadList.ContainsKey(pl.Id))
            {
                ThreadList[pl.Id].Abort();
                ThreadList.Remove(pl.Id);
            }
        }

        private void doLogic()
        {

        }
    }
}
