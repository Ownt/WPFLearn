using System.Collections.Generic;

namespace WPFLearn.Models
{
    internal class ProvisionOfServices
    {
        public string? Name { get; set; }

        public ICollection<Service>? Services { get; set; }

        public Clients? Clients { get; set; }
    }
}
