using Smart_Warehouse.Commons;
using Smart_Warehouse.Models.Interface;
using Smart_Warehouse.Services.SQLService;
using System.Reflection;
using static Smart_Warehouse.Services.SQLService.SQLServerServices;

namespace Smart_Warehouse.Models
{
    public class Pallet : IPropertyHandler
    {
        public static ISQLService<Pallet> PalLetSQL = new PalletSQL();

        public Propertyy PLID { get; set; } = new() { DBName = DBName.PLID, Type = typeof(int), AlowDatabase = false }; // ID
        public Propertyy VTLKID { get; set; } = new() { DBName = DBName.VTLKID, DisplayName = DispName.VTLKID, Type = typeof(int), AlowDatabase = true };
        public Propertyy PALLETCODE { get; set; } = new() { DBName = DBName.PALLETCODE, DisplayName = DispName.PALLETCODE, Type = typeof(string), AlowDatabase = true };

        public List<ITem> ITems { get; set; } = new();

        public static class DBName
        {
            public const string Table_Pallet = "KHO_Pallet";
            public const string PLID = "PLID";
            public const string VTLKID = "VTLKID";
            public const string PALLETCODE = "palletcode";
        }

        public static class DispName
        {
            public const string PLID = "PLID";
            public const string VTLKID = "VTLKID";
            public const string PALLETCODE = "palletcode";
        }

        // Get all property of this class
        public static List<Propertyy> GetClassProperties()
        {
            Type propertyType = typeof(Pallet);

            Pallet instance = new();

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
            Type propertyType = typeof(Pallet);

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
