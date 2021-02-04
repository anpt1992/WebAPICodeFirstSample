using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_system.Services.Base
{
    public class Filter
    {
        public bool isPaged { get; set; } = true;
        public int? page_size { get; set; }
        public int? page_number { get; set; }
    }
}
