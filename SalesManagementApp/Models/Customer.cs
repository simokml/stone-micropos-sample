using System;
using System.ComponentModel;

namespace SalesManagementApp.Models
{
    /// <summary>
    /// نموذج العميل
    /// </summary>
    public class Customer : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _email;
        private string _address;
        private decimal _balance;
        private DateTime _createdDate;
        private bool _isActive;

        /// <summary>
        /// معرف العميل
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        /// <summary>
        /// اسم العميل
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// رقم الهاتف
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        /// <summary>
        /// البريد الإلكتروني
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// العنوان
        /// </summary>
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        /// <summary>
        /// الرصيد الحالي (موجب = دائن، سالب = مدين)
        /// </summary>
        public decimal Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
                OnPropertyChanged(nameof(BalanceStatus));
            }
        }

        /// <summary>
        /// حالة الرصيد
        /// </summary>
        public string BalanceStatus
        {
            get
            {
                if (Balance > 0)
                    return "دائن";
                else if (Balance < 0)
                    return "مدين";
                else
                    return "متوازن";
            }
        }

        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime CreatedDate
        {
            get => _createdDate;
            set
            {
                _createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        /// <summary>
        /// هل العميل نشط
        /// </summary>
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

        /// <summary>
        /// نص الحالة
        /// </summary>
        public string StatusText => IsActive ? "نشط" : "غير نشط";

        /// <summary>
        /// منشئ افتراضي
        /// </summary>
        public Customer()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            Balance = 0;
        }

        /// <summary>
        /// منشئ مع المعاملات
        /// </summary>
        public Customer(string name, string phone, string email = "", string address = "")
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            CreatedDate = DateTime.Now;
            IsActive = true;
            Balance = 0;
        }

        /// <summary>
        /// حدث تغيير الخاصية
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// إثارة حدث تغيير الخاصية
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// تحديث الرصيد
        /// </summary>
        public void UpdateBalance(decimal amount)
        {
            Balance += amount;
        }

        /// <summary>
        /// التحقق من صحة البيانات
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && 
                   !string.IsNullOrWhiteSpace(Phone);
        }

        /// <summary>
        /// نسخ العميل
        /// </summary>
        public Customer Clone()
        {
            return new Customer
            {
                Id = this.Id,
                Name = this.Name,
                Phone = this.Phone,
                Email = this.Email,
                Address = this.Address,
                Balance = this.Balance,
                CreatedDate = this.CreatedDate,
                IsActive = this.IsActive
            };
        }

        /// <summary>
        /// تمثيل نصي للعميل
        /// </summary>
        public override string ToString()
        {
            return $"{Name} - {Phone}";
        }
    }
}
