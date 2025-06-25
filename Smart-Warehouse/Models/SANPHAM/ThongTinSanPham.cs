using Smart_Warehouse.Commons;
using Smart_Warehouse.Models.Interface;
using Smart_Warehouse.Services.SQLService;
using System.Reflection;
using static Smart_Warehouse.Services.SQLService.SQLServerServices;

namespace Smart_Warehouse.Models.SANPHAM
{
    public class ThongTinSanPham : IPropertyHandler
    {
        public static ISQLService<ThongTinSanPham> TTSPhamSQL = new TTSPhamSQL();

        public Propertyy TTSPID { get; set; } = new() { DBName = DBName.TTSPID, Type = typeof(int), AlowDatabase = false }; // ID
        public Propertyy SPID { get; set; } = new() { DBName = DBName.SPID, DisplayName = DispName.SPID, Type = typeof(int), AlowDatabase = true };
        public Propertyy LoaiTTSPID { get; set; } = new() { DBName = DBName.LoaiTTSPID, DisplayName = DispName.LoaiTTSPID, Type = typeof(int), AlowDatabase = true };
        public Propertyy GIATRI { get; set; } = new() { DBName = DBName.GIATRI, DisplayName = DispName.GIATRI, Type = typeof(string), AlowDatabase = true };

        public static class DBName
        {
            public const string Table_TTSanPham = "SP_ThongTinSanPham";
            public const string TTSPID = "TTSPID";
            public const string SPID = "SPID";
            public const string LoaiTTSPID = "LoaiTTSPID";
            public const string GIATRI = "giatri";
        }

        public static class DispName
        {
            public const string TTSPID = "TTSPID";
            public const string SPID = "SPID";
            public const string LoaiTTSPID = "LoaiTTSPID";
            public const string GIATRI = "giatri";
        }

        // Get all property of this class
        public static List<Propertyy> GetClassProperties()
        {
            Type propertyType = typeof(ThongTinSanPham);

            ThongTinSanPham instance = new();

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
            Type propertyType = typeof(ThongTinSanPham);

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
