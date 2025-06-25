namespace Smart_Warehouse.Models
{
    public class VTBlock
    {
        public List<VTFLoor> VTFLoors { get; set; } = new();

        public int? BlockID { get; set; }
        public string BlockCode { get; set; } = string.Empty;

        public class VTFLoor
        {
            public int? FloorID { get; set; }
            public string FloorCode { get; set; } = string.Empty;
            public List<VTriLuuKho> VTriLuuKhos { get; set; } = new();
        }
    }
}
