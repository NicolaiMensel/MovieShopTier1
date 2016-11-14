using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDLL.Entities
{
    public class Order : AbstractEntity
    {

        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
