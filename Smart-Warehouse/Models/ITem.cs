using Smart_Warehouse.Commons;
using Smart_Warehouse.Models.Interface;
using Smart_Warehouse.Services.SQLService;
using System.Reflection;
using static Smart_Warehouse.Services.SQLService.SQLServerServices;

namespace Smart_Warehouse.Models
{
    public class ITem : IPropertyHandler
    {
        public static ISQLService<ITem> ItemSQL = new ItemSQL();

        public Propertyy ITID { get; set; } = new() { DBName = DBName.ITID, Type = typeof(int), AlowDatabase = false }; // ID
        public Propertyy PLID { get; set; } = new() { DBName = DBName.PLID, DisplayName = DispName.PLID, Type = typeof(int), AlowDatabase = true };
        public Propertyy VTLKID { get; set; } = new() { DBName = DBName.VTLKID, DisplayName = DispName.VTLKID, Type = typeof(int), AlowDatabase = true };
        public Propertyy SPID { get; set; } = new() { DBName = DBName.SPID, DisplayName = DispName.SPID, Type = typeof(int), AlowDatabase = true };
        public Propertyy SOLUONG { get; set; } = new() { DBName = DBName.SOLUONG, DisplayName = DispName.SOLUONG, Type = typeof(int), AlowDatabase = true };
        public Propertyy NGAYNHAP { get; set; } = new() { DBName = DBName.NGAYNHAP, DisplayName = DispName.NGAYNHAP, Type = typeof(string), AlowDatabase = true, IsCheckSameValue = true };
        public Propertyy ITEMCODE { get; set; } = new() { DBName = DBName.ITEMCODE, DisplayName = DispName.ITEMCODE, Type = typeof(string), AlowDatabase = true, IsCheckSameValue = true };

        public object? TenSanPham { get; set; } = null; // Ten san pham
        public object? MaSanPham { get; set; } = null; // Ma san pham


        public static class DBName
        {
            public const string Table_Item = "KHO_Item";
            public const string ITID = "ITID";
            public const string PLID = "PLID";
            public const string VTLKID = "VTLKID";
            public const string SPID = "SPID";
            public const string SOLUONG = "soluong";
            public const string NGAYNHAP = "ngaynhap";
            public const string ITEMCODE = "itemcode";
        }

        public static class DispName
        {
            public const string ITID = "ITID";
            public const string PLID = "PLID";
            public const string VTLKID = "VTLKID";
            public const string SPID = "SPID";
            public const string SOLUONG = "Số lượng";
            public const string NGAYNHAP = "Ngày nhập";
            public const string ITEMCODE = "Item code";
        }

        // Get all property of this class
        public static List<Propertyy> GetClassProperties()
        {
            Type propertyType = typeof(ITem);

            ITem instance = new();

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
            Type propertyType = typeof(ITem);

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
