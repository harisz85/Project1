using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Models
{
    interface Response
    {
        bool success { get; set; }

        List<Product> data { get; set; }
    }
}
