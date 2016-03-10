using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class InventoryCountModel
    {
        public bool UpdateLocationInventory(int serviceProviderID, int userID,
            LocationProductCounts locationProductCounts)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                if (locationProductCounts == null)
                    return false;

                foreach(var inventoryItem in locationProductCounts.ProductCounts)
                {
                    var inventoryCount = new InventoryCount();
                    inventoryCount.Id = db.InventoryCounts.Max(ic => ic.Id) + 1;
                    inventoryCount.inventoryLocationId = inventoryItem.InventoryLocationID;
                    inventoryCount.userId = userID;
                    inventoryCount.uomId = inventoryItem.UOMID;
                    inventoryCount.productId = inventoryItem.ProductID;
                    inventoryCount.count = inventoryItem.Count;
                    inventoryCount.updateDate = locationProductCounts.DateRequested;
                    db.InventoryCounts.Add(inventoryCount);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
                }
                return true;
            }
        }
    }
}