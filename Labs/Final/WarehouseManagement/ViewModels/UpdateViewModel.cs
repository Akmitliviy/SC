using System.Collections.Generic;

namespace WarehouseManagement.Models
{
    public class UpdateViewModel
    {
        public string EntityType { get; set; }
        public int Id { get; set; }
        public Dictionary<string, object> Properties { get; set; }

        public UpdateViewModel()
        {
            Properties = new Dictionary<string, object>();
        }
    }
}
