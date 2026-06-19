using MiniERP.Application.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Services
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _saleRepository.GetActiveCustomersAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _saleRepository.GetActiveProductsAsync();
        }

        public async Task<(bool Success, string Message)> CreateAsync(
            DateTime saleDate,
            int customerId,
            List<(int ProductId, int Quantity, decimal UnitPrice)> details)
        {
            if (details == null || details.Count == 0)
            {
                return (false, "At least one sale detail is required.");
            }

            if (details.GroupBy(x => x.ProductId).Any(g => g.Count() > 1))
            {
                return (false, "A product cannot be repeated in the same sale.");
            }

            var saleDetails = new List<SaleDetail>();
            decimal totalAmount = 0;

            foreach (var detail in details)
            {
                if (detail.Quantity <= 0)
                {
                    return (false, "Quantity must be greater than zero.");
                }

                if (detail.UnitPrice <= 0)
                {
                    return (false, "Unit price must be greater than zero.");
                }

                var product = await _saleRepository.GetProductByIdAsync(detail.ProductId);
                if (product == null)
                {
                    return (false, "One of the selected products does not exist.");
                }

                if (product.Stock < detail.Quantity)
                {
                    return (false, $"Insufficient stock for product '{product.Name}'.");
                }

                product.Stock -= detail.Quantity;

                var subTotal = detail.Quantity * detail.UnitPrice;
                totalAmount += subTotal;

                saleDetails.Add(new SaleDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    SubTotal = subTotal
                });
            }

            var sale = new Sale
            {
                SaleDate = saleDate,
                CustomerId = customerId,
                TotalAmount = totalAmount,
                Details = saleDetails
            };

            await _saleRepository.AddAsync(sale);
            await _saleRepository.SaveChangesAsync();

            return (true, "Sale created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateAsync(
            int id,
            DateTime saleDate,
            int customerId,
            List<(int ProductId, int Quantity, decimal UnitPrice)> details)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                return (false, "Sale not found.");
            }

            if (details == null || details.Count == 0)
            {
                return (false, "At least one sale detail is required.");
            }

            if (details.GroupBy(x => x.ProductId).Any(g => g.Count() > 1))
            {
                return (false, "A product cannot be repeated in the same sale.");
            }

            foreach (var existingDetail in sale.Details)
            {
                existingDetail.Product.Stock += existingDetail.Quantity;
            }

            var newDetails = new List<SaleDetail>();
            decimal totalAmount = 0;

            foreach (var detail in details)
            {
                if (detail.Quantity <= 0)
                {
                    return (false, "Quantity must be greater than zero.");
                }

                if (detail.UnitPrice <= 0)
                {
                    return (false, "Unit price must be greater than zero.");
                }

                var product = await _saleRepository.GetProductByIdAsync(detail.ProductId);
                if (product == null)
                {
                    return (false, "One of the selected products does not exist.");
                }

                if (product.Stock < detail.Quantity)
                {
                    return (false, $"Insufficient stock for product '{product.Name}'.");
                }

                product.Stock -= detail.Quantity;

                var subTotal = detail.Quantity * detail.UnitPrice;
                totalAmount += subTotal;

                newDetails.Add(new SaleDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    SubTotal = subTotal
                });
            }

            sale.SaleDate = saleDate;
            sale.CustomerId = customerId;
            sale.TotalAmount = totalAmount;
            sale.Details = newDetails;

            await _saleRepository.UpdateAsync(sale);
            await _saleRepository.SaveChangesAsync();

            return (true, "Sale updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                return (false, "Sale not found.");
            }

            foreach (var detail in sale.Details)
            {
                detail.Product.Stock += detail.Quantity;
            }

            await _saleRepository.DeleteAsync(id);
            await _saleRepository.SaveChangesAsync();

            return (true, "Sale deleted successfully.");
        }
    }
}
