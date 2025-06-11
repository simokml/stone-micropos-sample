using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SalesManagementApp.Models
{
    public enum InvoiceType { Sale, Purchase, Return }
    public enum InvoiceStatus { Draft, Confirmed, Paid, Cancelled }

    /// <summary>
    /// نموذج الفاتورة
    /// </summary>
    public class Invoice : INotifyPropertyChanged
    {
        private int _id;
        private string _invoiceNumber;
        private DateTime _invoiceDate;
        private InvoiceType _type;
        private InvoiceStatus _status;
        private int _customerId;
        private string _customerName;
        private decimal _subtotal;
        private decimal _discountAmount;
        private decimal _discountPercentage;
        private decimal _taxAmount;
        private decimal _taxPercentage;
        private decimal _total;
        private decimal _paidAmount;
        private decimal _remainingAmount;
        private string _notes;
        private DateTime _createdDate;
        private string _createdBy;
        private List<InvoiceItem> _items;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string InvoiceNumber
        {
            get => _invoiceNumber;
            set { _invoiceNumber = value; OnPropertyChanged(nameof(InvoiceNumber)); }
        }

        public DateTime InvoiceDate
        {
            get => _invoiceDate;
            set { _invoiceDate = value; OnPropertyChanged(nameof(InvoiceDate)); }
        }

        public InvoiceType Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(nameof(Type)); OnPropertyChanged(nameof(TypeText)); }
        }

        public InvoiceStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); OnPropertyChanged(nameof(StatusText)); }
        }

        public int CustomerId
        {
            get => _customerId;
            set { _customerId = value; OnPropertyChanged(nameof(CustomerId)); }
        }

        public string CustomerName
        {
            get => _customerName;
            set { _customerName = value; OnPropertyChanged(nameof(CustomerName)); }
        }

        public decimal Subtotal
        {
            get => _subtotal;
            set { _subtotal = value; OnPropertyChanged(nameof(Subtotal)); CalculateTotal(); }
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
                DiscountAmount = Subtotal * (value / 100);
            }
        }

        public decimal TaxAmount
        {
            get => _taxAmount;
            set { _taxAmount = value; OnPropertyChanged(nameof(TaxAmount)); CalculateTotal(); }
        }

        public decimal TaxPercentage
        {
            get => _taxPercentage;
            set
            {
                _taxPercentage = value;
                OnPropertyChanged(nameof(TaxPercentage));
                TaxAmount = (Subtotal - DiscountAmount) * (value / 100);
            }
        }

        public decimal Total
        {
            get => _total;
            set { _total = value; OnPropertyChanged(nameof(Total)); CalculateRemainingAmount(); }
        }

        public decimal PaidAmount
        {
            get => _paidAmount;
            set { _paidAmount = value; OnPropertyChanged(nameof(PaidAmount)); CalculateRemainingAmount(); }
        }

        public decimal RemainingAmount
        {
            get => _remainingAmount;
            set { _remainingAmount = value; OnPropertyChanged(nameof(RemainingAmount)); }
        }

        public string Notes
        {
            get => _notes;
            set { _notes = value; OnPropertyChanged(nameof(Notes)); }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set { _createdDate = value; OnPropertyChanged(nameof(CreatedDate)); }
        }

        public string CreatedBy
        {
            get => _createdBy;
            set { _createdBy = value; OnPropertyChanged(nameof(CreatedBy)); }
        }

        public List<InvoiceItem> Items
        {
            get => _items ?? (_items = new List<InvoiceItem>());
            set { _items = value; OnPropertyChanged(nameof(Items)); CalculateSubtotal(); }
        }

        public string TypeText
        {
            get
            {
                switch (Type)
                {
                    case InvoiceType.Sale: return "مبيعات";
                    case InvoiceType.Purchase: return "مشتريات";
                    case InvoiceType.Return: return "مرتجعات";
                    default: return "غير محدد";
                }
            }
        }

        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case InvoiceStatus.Draft: return "مسودة";
                    case InvoiceStatus.Confirmed: return "مؤكدة";
                    case InvoiceStatus.Paid: return "مدفوعة";
                    case InvoiceStatus.Cancelled: return "ملغية";
                    default: return "غير محدد";
                }
            }
        }

        public int ItemsCount => Items?.Count ?? 0;

        public Invoice()
        {
            InvoiceDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Type = InvoiceType.Sale;
            Status = InvoiceStatus.Draft;
            TaxPercentage = 15;
            Items = new List<InvoiceItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CalculateSubtotal() => Subtotal = Items?.Sum(item => item.Total) ?? 0;
        private void CalculateTotal() => Total = Subtotal - DiscountAmount + TaxAmount;
        private void CalculateRemainingAmount() => RemainingAmount = Total - PaidAmount;

        public void AddItem(InvoiceItem item) { Items.Add(item); CalculateSubtotal(); }
        public void RemoveItem(InvoiceItem item) { Items.Remove(item); CalculateSubtotal(); }

        public bool IsValid() => !string.IsNullOrWhiteSpace(InvoiceNumber) && CustomerId > 0 && Items.Count > 0 && Items.All(item => item.IsValid());

        public void Confirm() { if (Status == InvoiceStatus.Draft) Status = InvoiceStatus.Confirmed; }
        public void Cancel() => Status = InvoiceStatus.Cancelled;

        public override string ToString() => $"{InvoiceNumber} - {CustomerName} - {Total:C}";
    }
}
