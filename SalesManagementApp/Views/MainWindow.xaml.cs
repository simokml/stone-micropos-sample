using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace SalesManagementApp.Views
{
    /// <summary>
    /// النافذة الرئيسية لتطبيق إدارة المبيعات
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button _activeMenuButton;

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        /// <summary>
        /// تهيئة النافذة
        /// </summary>
        private void InitializeWindow()
        {
            // تعيين التاريخ الحالي
            CurrentDateText.Text = DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("ar-SA"));

            // تعيين الزر النشط الافتراضي
            _activeMenuButton = DashboardButton;

            // تحميل لوحة التحكم كصفحة افتراضية
            LoadDashboard();

            // تحديث رسالة الحالة
            UpdateStatusMessage("تم تحميل التطبيق بنجاح");
        }

        /// <summary>
        /// تحميل لوحة التحكم
        /// </summary>
        private void LoadDashboard()
        {
            try
            {
                var dashboardContent = new TextBlock
                {
                    Text = "مرحباً بك في نظام إدارة المبيعات\n\nلوحة التحكم قيد التطوير...",
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                MainContentArea.Content = dashboardContent;
                PageTitleText.Text = "لوحة التحكم";
                UpdateStatusMessage("تم تحميل لوحة التحكم");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحميل لوحة التحكم: {ex.Message}");
            }
        }

        /// <summary>
        /// تحميل صفحة الفواتير
        /// </summary>
        private void LoadInvoice()
        {
            try
            {
                var invoiceContent = new TextBlock
                {
                    Text = "صفحة فاتورة المبيعات\n\nقيد التطوير...",
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                MainContentArea.Content = invoiceContent;
                PageTitleText.Text = "فاتورة المبيعات";
                UpdateStatusMessage("تم تحميل صفحة الفواتير");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحميل صفحة الفواتير: {ex.Message}");
            }
        }

        /// <summary>
        /// تحميل صفحة العملاء
        /// </summary>
        private void LoadCustomers()
        {
            try
            {
                var customersContent = new TextBlock
                {
                    Text = "صفحة إدارة العملاء\n\nقيد التطوير...",
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                MainContentArea.Content = customersContent;
                PageTitleText.Text = "إدارة العملاء";
                UpdateStatusMessage("تم تحميل صفحة العملاء");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحميل صفحة العملاء: {ex.Message}");
            }
        }

        /// <summary>
        /// تحميل صفحة الموردين
        /// </summary>
        private void LoadSuppliers()
        {
            try
            {
                var suppliersContent = new TextBlock
                {
                    Text = "صفحة إدارة الموردين\n\nقيد التطوير...",
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                MainContentArea.Content = suppliersContent;
                PageTitleText.Text = "إدارة الموردين";
                UpdateStatusMessage("تم تحميل صفحة الموردين");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحميل صفحة الموردين: {ex.Message}");
            }
        }

        /// <summary>
        /// تحميل صفحة المخزون
        /// </summary>
        private void LoadInventory()
        {
            try
            {
                var inventoryContent = new TextBlock
                {
                    Text = "صفحة إدارة المخزون\n\nقيد التطوير...",
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                MainContentArea.Content = inventoryContent;
                PageTitleText.Text = "إدارة المخزون";
                UpdateStatusMessage("تم تحميل صفحة المخزون");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحميل صفحة المخزون: {ex.Message}");
            }
        }

        /// <summary>
        /// تحميل صفحة التقارير
        /// </summary>
        private void LoadReports()
        {
            try
            {
                var reportsContent = new TextBlock
                {
                    Text = "صفحة التقارير\n\nقيد التطوير...",
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                MainContentArea.Content = reportsContent;
                PageTitleText.Text = "التقارير";
                UpdateStatusMessage("تم تحميل صفحة التقارير");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحميل صفحة التقارير: {ex.Message}");
            }
        }

        /// <summary>
        /// تحميل صفحة الإعدادات
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                var settingsContent = new TextBlock
                {
                    Text = "صفحة الإعدادات\n\nقيد التطوير...",
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                MainContentArea.Content = settingsContent;
                PageTitleText.Text = "الإعدادات";
                UpdateStatusMessage("تم تحميل صفحة الإعدادات");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحميل صفحة الإعدادات: {ex.Message}");
            }
        }

        /// <summary>
        /// تحديث الزر النشط في القائمة
        /// </summary>
        private void UpdateActiveMenuButton(Button newActiveButton)
        {
            // إزالة النمط النشط من الزر السابق
            if (_activeMenuButton != null)
            {
                _activeMenuButton.Style = (Style)FindResource("SideMenuButtonStyle");
            }

            // تطبيق النمط النشط على الزر الجديد
            newActiveButton.Style = (Style)FindResource("ActiveSideMenuButtonStyle");
            _activeMenuButton = newActiveButton;
        }

        /// <summary>
        /// تحديث رسالة الحالة
        /// </summary>
        private void UpdateStatusMessage(string message)
        {
            StatusMessageText.Text = message;
        }

        /// <summary>
        /// عرض رسالة خطأ
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            UpdateStatusMessage("حدث خطأ");
        }

        /// <summary>
        /// عرض رسالة معلومات
        /// </summary>
        private void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, "معلومات", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // أحداث أزرار القائمة الجانبية
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateActiveMenuButton(DashboardButton);
            LoadDashboard();
        }

        private void InvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateActiveMenuButton(InvoiceButton);
            LoadInvoice();
        }

        private void CustomersButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateActiveMenuButton(CustomersButton);
            LoadCustomers();
        }

        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateActiveMenuButton(SuppliersButton);
            LoadSuppliers();
        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateActiveMenuButton(InventoryButton);
            LoadInventory();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateActiveMenuButton(ReportsButton);
            LoadReports();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateActiveMenuButton(SettingsButton);
            LoadSettings();
        }

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowInfoMessage("ميزة النسخ الاحتياطي قيد التطوير");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في فتح نافذة النسخ الاحتياطي: {ex.Message}");
            }
        }

        // أحداث أزرار شريط العنوان
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // إعادة تحميل الصفحة الحالية
                if (_activeMenuButton == DashboardButton)
                    LoadDashboard();
                else if (_activeMenuButton == InvoiceButton)
                    LoadInvoice();
                else if (_activeMenuButton == CustomersButton)
                    LoadCustomers();
                else if (_activeMenuButton == SuppliersButton)
                    LoadSuppliers();
                else if (_activeMenuButton == InventoryButton)
                    LoadInventory();
                else if (_activeMenuButton == ReportsButton)
                    LoadReports();
                else if (_activeMenuButton == SettingsButton)
                    LoadSettings();

                UpdateStatusMessage("تم تحديث الصفحة");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"خطأ في تحديث الصفحة: {ex.Message}");
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var helpMessage = @"مرحباً بك في نظام إدارة المبيعات

الأقسام المتاحة:
• الرئيسية: لوحة تحكم تعرض ملخص العمليات
• فاتورة المبيعات: إنشاء وإدارة فواتير البيع
• العملاء: إدارة بيانات العملاء
• الموردون: إدارة بيانات الموردين
• المخزون: إدارة المنتجات والمخزون
• التقارير: عرض التقارير المالية والإحصائية
• الإعدادات: ضبط إعدادات النظام
• النسخ الاحتياطي: إنشاء واستعادة النسخ الاحتياطية

للمساعدة الفنية، يرجى التواصل مع فريق الدعم.";

            ShowInfoMessage(helpMessage);
        }
    }
}
