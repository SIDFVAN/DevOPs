using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Shared.Products
{
    public class ProductResult
    {
        public class Saved
        {
            public Guid ProductId { get; set; }
            public string UploadUri { get; set; } = default!;
        }
    }
}
