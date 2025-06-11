using System;
using System.ComponentModel;

namespace SalesManagementApp.Models
{
    /// <summary>
    /// نموذج المنتج
    /// </summary>
    public class Product : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _barcode;
        private string _description;
        private decimal _purchasePrice;
        private decimal _salePrice;
        private int _quantity;
        private int _minQuantity;
        private string _unit;
        private string _category;
        private DateTime _createdDate;
        private bool _isActive;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Barcode
        {
            get => _barcode;
            set { _barcode = value; OnPropertyChanged(nameof(Barcode)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public decimal PurchasePrice
        {
            get => _purchasePrice;
            set
            {
                _purchasePrice = value;
                OnPropertyChanged(nameof(PurchasePrice));
                OnPropertyChanged(nameof(ProfitMargin));
                OnPropertyChanged(nameof(ProfitPercentage));
            }
        }

        public decimal SalePrice
        {
            get => _salePrice;
            set
            {
                _salePrice = value;
                OnPropertyChanged(nameof(SalePrice));
                OnPropertyChanged(nameof(ProfitMargin));
                OnPropertyChanged(nameof(ProfitPercentage));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(IsLowStock));
                OnPropertyChanged(nameof(StockStatus));
            }
        }

        public int MinQuantity
        {
            get => _minQuantity;
            set
            {
                _minQuantity = value;
                OnPropertyChanged(nameof(MinQuantity));
                OnPropertyChanged(nameof(IsLowStock));
                OnPropertyChanged(nameof(StockStatus));
            }
        }

        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(nameof(Unit)); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set { _createdDate = value; OnPropertyChanged(nameof(CreatedDate)); }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public decimal ProfitMargin => SalePrice - PurchasePrice;
        public decimal ProfitPercentage => PurchasePrice > 0 ? (ProfitMargin / PurchasePrice) * 100 : 0;
        public bool IsLowStock => Quantity <= MinQuantity;
        public string StockStatus
        {
            get
            {
                if (Quantity == 0) return "نفد المخزون";
                else if (IsLowStock) return "مخزون منخفض";
                else return "متوفر";
            }
        }
        public string StatusText => IsActive ? "نشط" : "غير نشط";
        public decimal TotalValue => Quantity * PurchasePrice;

        public Product()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            Unit = "قطعة";
            MinQuantity = 5;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateQuantity(int amount) => Quantity += amount;
        public bool IsQuantityAvailable(int requestedQuantity) => Quantity >= requestedQuantity;
        public bool IsValid() => !string.IsNullOrWhiteSpace(Name) && SalePrice > 0 && PurchasePrice >= 0;

        public override string ToString() => $"{Name} - {SalePrice:C}";
    }
}
