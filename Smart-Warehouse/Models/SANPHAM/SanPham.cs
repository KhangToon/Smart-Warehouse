using Smart_Warehouse.Commons;
using Smart_Warehouse.Models.Interface;
using Smart_Warehouse.Services.SQLService;
using System.Reflection;
using static Smart_Warehouse.Services.SQLService.SQLServerServices;

namespace Smart_Warehouse.Models.SANPHAM
{
    public class SanPham : IPropertyHandler
    {
        public static ISQLService<SanPham> SPSQL = new SanPhamSQL();

        public Propertyy SPID { get; set; } = new() { DBName = DBName.SPID, Type = typeof(int), AlowDatabase = false }; // ID
        public Propertyy MASANPHAM { get; set; } = new() { DBName = DBName.MASANPHAM, DisplayName = DispName.MASANPHAM, Type = typeof(string), AlowDatabase = true };
        public Propertyy TENSANPHAM { get; set; } = new() { DBName = DBName.TENSANPHAM, DisplayName = DispName.TENSANPHAM, Type = typeof(string), AlowDatabase = true };
        public Propertyy NCIDs { get; set; } = new() { DBName = DBName.NCIDs, DisplayName = DispName.NCIDs, Type = typeof(string), AlowDatabase = true };

        public static class DBName
        {
            public const string Table_SanPham = "SPSanPham";
            public const string SPID = "SPID";
            public const string MASANPHAM = "masanpham";
            public const string TENSANPHAM = "tensanpham";
            public const string NCIDs = "NCIDs";
        }

        public static class DispName
        {
            public const string MASANPHAM = "Mã sản phẩm";
            public const string TENSANPHAM = "Tên sản phẩm";
            public const string NCIDs = "NCIDs";
        }

        // Get all property of this class
        public static List<Propertyy> GetClassProperties()
        {
            Type propertyType = typeof(SanPham);

            SanPham instance = new();

            FieldInfo[] fields = propertyType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            List<Propertyy> propertiesValue = new();

            foreach (FieldInfo field in fields)
            {
                Type ob = field.FieldType;

                if (ob == typeof(Propertyy))
                {
                    Propertyy? fieldValue = (Propertyy?)field.GetValue(instance);

                    if (fieldValue != null)
                    {
                        propertiesValue.Add(fieldValue);
                    }
                }
            }

            return propertiesValue;
        }

        public List<Propertyy> GetPropertiesValues()
        {
            Type propertyType = typeof(SanPham);

            FieldInfo[] fields = propertyType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            List<Propertyy> propertiesValue = new();

            foreach (FieldInfo field in fields)
            {
                Type ob = field.FieldType;

                if (ob == typeof(Propertyy))
                {
                    Propertyy? fieldValue = (Propertyy?)field.GetValue(this);

                    if (fieldValue != null) { propertiesValue.Add(fieldValue); }
                }
            }

            return propertiesValue;
        }

        public void SetPropertyValue(string propertyName, object newValue)
        {
            List<Propertyy> propertiesValue = GetPropertiesValues();

            Propertyy? tagetProperty = propertiesValue.FirstOrDefault(f => f.DBName == propertyName);

            if (tagetProperty != null)
            {
                tagetProperty.Value = newValue;
            }
        }

        public object? GetPropertyValue(string propertyName)
        {
            List<Propertyy> propertiesValue = GetPropertiesValues();

            Propertyy? tagetProperty = propertiesValue.FirstOrDefault(f => f.DBName == propertyName);

            return tagetProperty?.Value;
        }
    }
}
