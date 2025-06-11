using System;
using System.ComponentModel;

namespace SalesManagementApp.Models
{
    /// <summary>
    /// نموذج عنصر الفاتورة
    /// </summary>
    public class InvoiceItem : INotifyPropertyChanged
    {
        private int _id;
        private int _invoiceId;
        private int _productId;
        private string _productName;
        private string _productBarcode;
        private decimal _unitPrice;
        private int _quantity;
        private decimal _discountAmount;
        private decimal _discountPercentage;
        private decimal _total;
        private string _notes;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int InvoiceId
        {
            get => _invoiceId;
            set { _invoiceId = value; OnPropertyChanged(nameof(InvoiceId)); }
        }

        public int ProductId
        {
            get => _productId;
            set { _productId = value; OnPropertyChanged(nameof(ProductId)); }
        }

        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        public string ProductBarcode
        {
            get => _productBarcode;
            set { _productBarcode = value; OnPropertyChanged(nameof(ProductBarcode)); }
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            set { _unitPrice = value; OnPropertyChanged(nameof(UnitPrice)); CalculateTotal(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); CalculateTotal(); }
        }

        public decimal DiscountAmount
        {
            get => _discountAmount;
            set { _discountAmount = value; OnPropertyChanged(nameof(DiscountAmount)); CalculateTotal(); }
        }

        public decimal DiscountPercentage
        {
            get => _discountPercentage;
            set
            {
                _discountPercentage = value;
                OnPropertyChanged(nameof(DiscountPercentage));
                var subtotal = UnitPrice * Quantity;
                DiscountAmount = subtotal * (value / 100);
            }
        }

        public decimal Total
        {
            get => _total;
            set { _total = value; OnPropertyChanged(nameof(Total)); }
        }

        public string Notes
        {
            get => _notes;
            set { _notes = value; OnPropertyChanged(nameof(Notes)); }
        }

        public decimal Subtotal => UnitPrice * Quantity;

        public InvoiceItem()
        {
            Quantity = 1;
        }

        public InvoiceItem(int productId, string productName, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            CalculateTotal();
        }

        public InvoiceItem(Product product, int quantity)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            ProductBarcode = product.Barcode;
            UnitPrice = product.SalePrice;
            Quantity = quantity;
            CalculateTotal();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CalculateTotal()
        {
            Total = (UnitPrice * Quantity) - DiscountAmount;
        }

        public void ApplyDiscountPercentage(decimal percentage) => DiscountPercentage = percentage;

        public void ApplyDiscountAmount(decimal amount)
        {
            DiscountAmount = amount;
            if (Subtotal > 0)
                DiscountPercentage = (amount / Subtotal) * 100;
        }

        public void IncreaseQuantity(int amount = 1) => Quantity += amount;

        public void DecreaseQuantity(int amount = 1)
        {
            if (Quantity > amount)
                Quantity -= amount;
            else
                Quantity = 0;
        }

        public bool IsValid() => ProductId > 0 && !string.IsNullOrWhiteSpace(ProductName) && UnitPrice > 0 && Quantity > 0;

        public InvoiceItem Clone()
        {
            return new InvoiceItem
            {
                Id = this.Id,
                InvoiceId = this.InvoiceId,
                ProductId = this.ProductId,
                ProductName = this.ProductName,
                ProductBarcode = this.ProductBarcode,
                UnitPrice = this.UnitPrice,
                Quantity = this.Quantity,
                DiscountAmount = this.DiscountAmount,
                DiscountPercentage = this.DiscountPercentage,
                Total = this.Total,
                Notes = this.Notes
            };
        }

        public override string ToString() => $"{ProductName} x {Quantity} = {Total:C}";
    }
}
