using Smart_Warehouse.Commons;
using Smart_Warehouse.Models.Interface;
using Smart_Warehouse.Services.SQLService;
using System.Reflection;
using static Smart_Warehouse.Services.SQLService.SQLServerServices;

namespace Smart_Warehouse.Models
{
    public class VTriLuuKho : IPropertyHandler
    {
        public static ISQLService<VTriLuuKho> VTLKSQL = new VTLKSQL();

        public Propertyy VTLKID { get; set; } = new() { DBName = DBName.VTLKID, Type = typeof(int), AlowDatabase = false, AlowDisplay = false }; // ID
        public Propertyy PLID { get; set; } = new() { DBName = DBName.PLID, DisplayName = DispName.PLID, Type = typeof(int), AlowDatabase = true, AlowDisplay = false };
        public Propertyy MAVITRI { get; set; } = new() { DBName = DBName.MaViTri, DisplayName = DispName.MAVITRI, Type = typeof(string), AlowDatabase = true, IsCheckSameValue = true };
        public Propertyy DISPLAYINDEX { get; set; } = new() { DBName = DBName.DISPLAYINDEX, DisplayName = DispName.DISPLAYINDEX, Type = typeof(int), AlowDatabase = true, AlowDisplay = false };
        public Propertyy BLOCK { get; set; } = new() { DBName = DBName.BLOCK, DisplayName = DispName.BLOCK, Type = typeof(string), AlowDatabase = true };
        public Propertyy FLOOR { get; set; } = new() { DBName = DBName.FLOOR, DisplayName = DispName.FLOOR, Type = typeof(string), AlowDatabase = true };
        public Propertyy XLOCATION { get; set; } = new() { DBName = DBName.XLOCATION, DisplayName = DispName.XLOCATION, Type = typeof(string), AlowDatabase = true };
        public Propertyy YLOCATION { get; set; } = new() { DBName = DBName.YLOCATION, DisplayName = DispName.YLOCATION, Type = typeof(string), AlowDatabase = true };

        public Pallet? TargetPallet { get; set; } = null;

        public static class DBName
        {
            public const string Table_VTriLuuKho = "KHO_VTriLuuKho";
            public const string VTLKID = "VTLKID";
            public const string PLID = "PLID";
            public const string MaViTri = "vitricode";
            public const string DISPLAYINDEX = "displayindex";
            public const string BLOCK = "block";
            public const string FLOOR = "floor";
            public const string XLOCATION = "xlocation";
            public const string YLOCATION = "ylocation";
        }

        private class DispName
        {
            public const string VTLKID = "VTLKID";
            public const string PLID = "PLID";
            public const string MAVITRI = "Mã vị trí";
            public const string DISPLAYINDEX = "DisplayIndex";
            public const string FLOOR = "FLOOR";
            public const string BLOCK = "BLOCK";
            public const string XLOCATION = "XLOCATION";
            public const string YLOCATION = "YLOCATION";
        }

        // Get all property of this class
        public static List<Propertyy> GetClassProperties()
        {
            Type propertyType = typeof(VTriLuuKho);

            VTriLuuKho instance = new();

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
            Type propertyType = typeof(VTriLuuKho);

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

        public static List<VTBlock> RangeVTLuuKhoToLayout(List<VTriLuuKho> vTriLuuKhos)
        {
            List<VTBlock> vTBlocks = new();

            var blockGroups = vTriLuuKhos.GroupBy(v => v.BLOCK.Value);

            foreach (var block in blockGroups)
            {
                VTBlock vTBlock = new()
                {
                    BlockID = int.TryParse(block.Key?.ToString(), out int blid) ? blid : 0,
                };

                var floorGroups = block.GroupBy(f => f.FLOOR.Value);

                foreach (var floor in floorGroups)
                {
                    VTBlock.VTFLoor vTFloor = new()
                    {
                        FloorID = int.TryParse(floor.Key?.ToString(), out int flid) ? flid : 0,

                        VTriLuuKhos = floor.ToList()
                    };

                    vTBlock.VTFLoors.Add(vTFloor);
                }

                vTBlocks.Add(vTBlock);
            }

            return vTBlocks;
        }
    }
}
