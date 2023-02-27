using System;
using System.Collections.Generic;

namespace WPFLearn.Models
{
    internal class Contracts
    {
        public string? Name { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Service>? Services { get; set; }

        public Clients? Clients { get; set; }
    }
}
