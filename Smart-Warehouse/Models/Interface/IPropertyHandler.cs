using Smart_Warehouse.Commons;

namespace Smart_Warehouse.Models.Interface
{
    public interface IPropertyHandler
    {
        // Get property values of the instance
        List<Propertyy> GetPropertiesValues();

        // Set the value of a specific property
        void SetPropertyValue(string propertyName, object newValue);

        // Get the value of a specific property
        object? GetPropertyValue(string propertyName);
    }
}
