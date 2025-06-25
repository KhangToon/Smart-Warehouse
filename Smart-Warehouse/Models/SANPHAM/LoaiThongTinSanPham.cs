using Smart_Warehouse.Commons;
using Smart_Warehouse.Models.Interface;
using Smart_Warehouse.Services.SQLService;
using System.Reflection;
using static Smart_Warehouse.Services.SQLService.SQLServerServices;

namespace Smart_Warehouse.Models.SANPHAM
{
    public class LoaiThongTinSanPham : IPropertyHandler
    {
        public static ISQLService<LoaiThongTinSanPham> LoaiTTSPSQL = new LoaiTTSPSQL();

        public Propertyy LoaiTTSPID { get; set; } = new() { DBName = DBName.LoaiTTSPID, DisplayName = DispName.LoaiTTSPID, Type = typeof(int), AlowDatabase = false, AlowDisplay = false, DispDatagrid = false }; // ID
        public Propertyy TenLoaiThongTin { get; set; } = new() { DBName = DBName.TenLoaiThongTin, DisplayName = DispName.TenLoaiThongTin, Type = typeof(string), AlowDatabase = true };
        public Propertyy KieuDuLieu { get; set; } = new() { DBName = DBName.KieuDuLieu, DisplayName = DispName.KieuDuLieu, Type = typeof(string), AlowDatabase = true };
        public Propertyy GiaTriMacDinh { get; set; } = new() { DBName = DBName.GiaTriMacDinh, DisplayName = DispName.GiaTriMacDinh, Type = typeof(string), AlowDatabase = true };
        public Propertyy IsDefault { get; set; } = new() { DBName = DBName.IsDefault, DisplayName = DispName.IsDefault, Type = typeof(bool), AlowDatabase = true };
        public Propertyy TenTruyXuat { get; set; } = new() { DBName = DBName.TenTruyXuat, DisplayName = DispName.TenTruyXuat, Type = typeof(string), AlowDatabase = true };
        public Propertyy DisplayIndex { get; set; } = new() { DBName = DBName.DisplayIndex, DisplayName = DispName.DisplayIndex, Type = typeof(int), AlowDatabase = true };

        public static class DBName
        {
            public const string Table_LoaiTTSP = "SP_LoaiThongTinSanPham";
            public const string LoaiTTSPID = "LoaiTTSPID";
            public const string TenLoaiThongTin = "GiaTriMacDinh";
            public const string KieuDuLieu = "KieuDuLieu";
            public const string GiaTriMacDinh = "GiaTriMacDinh";
            public const string IsDefault = "IsDefault";
            public const string TenTruyXuat = "TenTruyXuat";
            public const string DisplayIndex = "DisplayIndex";
        }

        public static class DispName
        {
            public const string LoaiTTSPID = "LoaiTTSPID";
            public const string TenLoaiThongTin = "GiaTriMacDinh";
            public const string KieuDuLieu = "KieuDuLieu";
            public const string GiaTriMacDinh = "GiaTriMacDinh";
            public const string IsDefault = "IsDefault";
            public const string TenTruyXuat = "TenTruyXuat";
            public const string DisplayIndex = "DisplayIndex";
        }

        // Get all property of this class
        public static List<Propertyy> GetClassProperties()
        {
            Type propertyType = typeof(LoaiThongTinSanPham);

            LoaiThongTinSanPham instance = new();

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
            Type propertyType = typeof(LoaiThongTinSanPham);

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
