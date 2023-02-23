using System.Collections.Generic;

namespace WPFLearn.Models
{
    class ProvisionOfServices
    {
        public string? Name { get; set; }

        public ICollection<Service>? Services { get; set; }

        public Service? service { get; set; }
    }
}
