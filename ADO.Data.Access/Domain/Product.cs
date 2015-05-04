using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ADO.Data.Access.Domain
{
    public class Product
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        [AllowHtml]
        public string ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
