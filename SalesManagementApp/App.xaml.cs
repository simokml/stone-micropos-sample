using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using SalesManagementApp.Services;

namespace SalesManagementApp
{
    /// <summary>
    /// تطبيق إدارة المبيعات
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // تعيين اللغة العربية
            SetArabicCulture();

            // تهيئة قاعدة البيانات
            InitializeDatabase();

            // تعيين معالج الأخطاء العامة
            SetupExceptionHandling();
        }

        /// <summary>
        /// تعيين اللغة العربية للتطبيق
        /// </summary>
        private void SetArabicCulture()
        {
            var culture = new CultureInfo("ar-SA");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            // تعيين اتجاه النص من اليمين لليسار
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }

        /// <summary>
        /// تهيئة قاعدة البيانات
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                var databaseService = new DatabaseService();
                databaseService.InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تهيئة قاعدة البيانات: {ex.Message}", 
                    "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        /// <summary>
        /// إعداد معالج الأخطاء العامة
        /// </summary>
        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;
                MessageBox.Show($"حدث خطأ غير متوقع: {exception?.Message}", 
                    "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            DispatcherUnhandledException += (sender, e) =>
            {
                MessageBox.Show($"حدث خطأ في واجهة المستخدم: {e.Exception.Message}", 
                    "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            };
        }
    }
}
