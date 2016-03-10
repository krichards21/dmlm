using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class LocationProductCount
    {
        public string LocationName { get; set; }
        public int LocationID { get; set; }
        //public List<InventoryLocationProducts> InventoryProductCounts { get; set; }
        public int InventoryLocationID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double? Count { get; set; }
        public int UOMID { get; set; }
    }

    public class LocationProductCounts
    {
        public DateTime DateRequested { get; set; }
        public List<LocationProductCount> ProductCounts { get; set; }
    }
    public class InventoryLocationsModel
    {
        public class InventoryLocation
        {
            public int InventoryLocationID { get; set; }
            public string InventoryLocationName { get; set; }
            public string InventoryLocationDescriptions { get; set; }
        }

        public class InventoryLocationProducts
        {
            public InventoryLocation InventoryLocation { get; set; }
            public List<InventoryCounts> InventoryCounts { get; set; }

        }

        public class InventoryCounts
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public double? Count { get; set; }
            public int UOMID { get; set; }
        }

        public LocationProductCounts GetInventoryCountsByLocation(int locationID)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var locationEntity = db.Locations.Find(locationID);
                var locationProductCounts = new LocationProductCounts();
                locationProductCounts.DateRequested = DateTime.UtcNow;
                locationProductCounts.ProductCounts = new List<LocationProductCount>();
                //inventoryCounts.InventoryProductCounts = new List<InventoryLocationProducts>();
                foreach (var inventoryLocation in locationEntity.InventoryLocations)
                {
                    foreach (var inventoryCount in inventoryLocation.InventoryCounts)
                    {
                        locationProductCounts.ProductCounts.Add(new LocationProductCount()
                        {
                            LocationID = locationEntity.Id,
                            LocationName = locationEntity.name,
                            InventoryLocationID = inventoryLocation.Id,

                            ProductID = inventoryCount.productId,
                            ProductName = inventoryCount.Product.name,
                            UOMID = inventoryCount.uomId,
                            Count = inventoryCount.count
                        });
                    }
                    //inventoryCounts.InventoryProductCounts.Add(new InventoryLocationProducts()
                    //{
                        //InventoryLocation = new InventoryLocation()
                        //{
                            //InventoryLocationID = inventoryLocation.Id,
                            //InventoryLocationName = inventoryLocation.name,
                            //InventoryLocationDescriptions = inventoryLocation.description
                        //},
                        //InventoryCounts = inventoryLocation.InventoryCounts.Select(ic => new InventoryCounts()
                        //{
                        //    ProductID = ic.productId,
                        //    ProductName = ic.Product.name,
                        //    Count = ic.count,
                        //    UOMID = ic.uomId
                        //}).ToList()
                    //});
                }
                return locationProductCounts;
            }
        }
    }
}