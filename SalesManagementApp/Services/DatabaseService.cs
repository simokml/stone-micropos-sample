using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SalesManagementApp.Services
{
    /// <summary>
    /// خدمة قاعدة البيانات
    /// </summary>
    public class DatabaseService
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public DatabaseService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SalesDatabase"].ConnectionString;
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "SalesDatabase.db");
        }

        public void InitializeDatabase()
        {
            try
            {
                var databaseDirectory = Path.GetDirectoryName(_databasePath);
                if (!Directory.Exists(databaseDirectory))
                    Directory.CreateDirectory(databaseDirectory);

                if (!File.Exists(_databasePath))
                    SQLiteConnection.CreateFile(_databasePath);

                CreateTables();
                InsertInitialData();
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تهيئة قاعدة البيانات: {ex.Message}", ex);
            }
        }

        private void CreateTables()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                var createCustomersTable = @"
                    CREATE TABLE IF NOT EXISTS Customers (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Phone TEXT NOT NULL,
                        Email TEXT,
                        Address TEXT,
                        Balance DECIMAL DEFAULT 0,
                        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                        IsActive BOOLEAN DEFAULT 1
                    )";

                var createProductsTable = @"
                    CREATE TABLE IF NOT EXISTS Products (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Barcode TEXT UNIQUE,
                        Description TEXT,
                        PurchasePrice DECIMAL NOT NULL DEFAULT 0,
                        SalePrice DECIMAL NOT NULL DEFAULT 0,
                        Quantity INTEGER NOT NULL DEFAULT 0,
                        MinQuantity INTEGER NOT NULL DEFAULT 5,
                        Unit TEXT DEFAULT 'قطعة',
                        Category TEXT,
                        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                        IsActive BOOLEAN DEFAULT 1
                    )";

                var createInvoicesTable = @"
                    CREATE TABLE IF NOT EXISTS Invoices (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        InvoiceNumber TEXT NOT NULL UNIQUE,
                        InvoiceDate DATETIME NOT NULL,
                        Type INTEGER NOT NULL DEFAULT 0,
                        Status INTEGER NOT NULL DEFAULT 0,
                        CustomerId INTEGER NOT NULL,
                        CustomerName TEXT NOT NULL,
                        Subtotal DECIMAL NOT NULL DEFAULT 0,
                        DiscountAmount DECIMAL NOT NULL DEFAULT 0,
                        DiscountPercentage DECIMAL NOT NULL DEFAULT 0,
                        TaxAmount DECIMAL NOT NULL DEFAULT 0,
                        TaxPercentage DECIMAL NOT NULL DEFAULT 15,
                        Total DECIMAL NOT NULL DEFAULT 0,
                        PaidAmount DECIMAL NOT NULL DEFAULT 0,
                        RemainingAmount DECIMAL NOT NULL DEFAULT 0,
                        Notes TEXT,
                        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                        CreatedBy TEXT,
                        FOREIGN KEY (CustomerId) REFERENCES Customers (Id)
                    )";

                var createInvoiceItemsTable = @"
                    CREATE TABLE IF NOT EXISTS InvoiceItems (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        InvoiceId INTEGER NOT NULL,
                        ProductId INTEGER NOT NULL,
                        ProductName TEXT NOT NULL,
                        ProductBarcode TEXT,
                        UnitPrice DECIMAL NOT NULL,
                        Quantity INTEGER NOT NULL,
                        DiscountAmount DECIMAL NOT NULL DEFAULT 0,
                        DiscountPercentage DECIMAL NOT NULL DEFAULT 0,
                        Total DECIMAL NOT NULL,
                        Notes TEXT,
                        FOREIGN KEY (InvoiceId) REFERENCES Invoices (Id),
                        FOREIGN KEY (ProductId) REFERENCES Products (Id)
                    )";

                var createSettingsTable = @"
                    CREATE TABLE IF NOT EXISTS Settings (
                        Key TEXT PRIMARY KEY,
                        Value TEXT NOT NULL,
                        Description TEXT
                    )";

                ExecuteNonQuery(connection, createCustomersTable);
                ExecuteNonQuery(connection, createProductsTable);
                ExecuteNonQuery(connection, createInvoicesTable);
                ExecuteNonQuery(connection, createInvoiceItemsTable);
                ExecuteNonQuery(connection, createSettingsTable);
            }
        }

        private void InsertInitialData()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                var checkData = "SELECT COUNT(*) FROM Settings WHERE Key = 'DatabaseInitialized'";
                var count = Convert.ToInt32(ExecuteScalar(connection, checkData));

                if (count == 0)
                {
                    var insertSettings = @"
                        INSERT INTO Settings (Key, Value, Description) VALUES
                        ('DatabaseInitialized', 'true', 'تم تهيئة قاعدة البيانات'),
                        ('CompanyName', 'شركة إدارة المبيعات', 'اسم الشركة'),
                        ('Currency', 'ريال سعودي', 'العملة'),
                        ('TaxRate', '15', 'معدل الضريبة'),
                        ('InvoicePrefix', 'INV', 'بادئة رقم الفاتورة'),
                        ('LastInvoiceNumber', '0', 'آخر رقم فاتورة')";

                    ExecuteNonQuery(connection, insertSettings);

                    var insertDefaultCustomer = @"
                        INSERT INTO Customers (Name, Phone, Email, Address) VALUES
                        ('عميل نقدي', '000000000', '', 'غير محدد')";

                    ExecuteNonQuery(connection, insertDefaultCustomer);

                    var insertSampleProducts = @"
                        INSERT INTO Products (Name, Barcode, PurchasePrice, SalePrice, Quantity, Category) VALUES
                        ('منتج تجريبي 1', '1234567890', 10.00, 15.00, 100, 'عام'),
                        ('منتج تجريبي 2', '1234567891', 20.00, 30.00, 50, 'عام'),
                        ('منتج تجريبي 3', '1234567892', 5.00, 8.00, 200, 'عام')";

                    ExecuteNonQuery(connection, insertSampleProducts);
                }
            }
        }

        private void ExecuteNonQuery(SQLiteConnection connection, string sql)
        {
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private object ExecuteScalar(SQLiteConnection connection, string sql)
        {
            using (var command = new SQLiteCommand(sql, connection))
            {
                return command.ExecuteScalar();
            }
        }

        public SQLiteConnection GetConnection() => new SQLiteConnection(_connectionString);

        public DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    return command.ExecuteScalar();
                }
            }
        }

        public void CreateBackup(string backupPath)
        {
            try
            {
                File.Copy(_databasePath, backupPath, true);
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في إنشاء النسخة الاحتياطية: {ex.Message}", ex);
            }
        }

        public void RestoreBackup(string backupPath)
        {
            try
            {
                if (File.Exists(backupPath))
                    File.Copy(backupPath, _databasePath, true);
                else
                    throw new FileNotFoundException("ملف النسخة الاحتياطية غير موجود");
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في استعادة النسخة الاحتياطية: {ex.Message}", ex);
            }
        }
    }
}
