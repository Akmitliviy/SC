namespace WarehouseManagement.Models
{
    public class AddViewModel
    {
        public string EntityType { get; set; }
        public Dictionary<string, object> Properties { get; set; }

        public AddViewModel()
        {
            Properties = new Dictionary<string, object>();
        }
    }
}
