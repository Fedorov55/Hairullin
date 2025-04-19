using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Hairullin.Entities
{
    
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public byte[] Image { get; set; } // BLOB data (массив байтов)
        [NotMapped]
            public ImageSource ImageSource { get; set; } // Для отображения в UI
        }
    
}
