using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class ProductModel
    {
        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public UOM UOM { get; set; }
            public DefaultUOM Default { get; set; }
            public string ProductDescription { get; set; }
        }

        public Product GetProduct(int serviceProviderID)
        {
            return new Product();
        }
    }
}