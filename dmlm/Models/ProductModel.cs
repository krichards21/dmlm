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

        public List<Product> GetProducts(int serviceProviderID)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var products = new List<Product>();
                var productEntity = db.Products.Where(l => l.serviceProviderId == serviceProviderID);
                foreach(var product in productEntity)
                {
                    products.Add(new Product
                    {
                        ProductName = product.name,
                        ProductDescription = product.description,
                        ProductID = product.Id,
                        UOM = product.UOM
                    });
                }
                return products;
            }
        }
    }
}