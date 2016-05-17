using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class AlertModel
    {
        public class Alert
        {
            public int AlertID { get; set; }
            public string Description { get; set; }
            public DateTime CreateDate { get; set; }
            public bool AlertType { get; set; }
        }

  
        public List<Alert> GetAlerts(int serviceProviderID, int userID)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var alertEntity = db.Alerts.Where(l => l.serviceProviderID == serviceProviderID);
                var alertList = new List<Alert>();
                foreach (var alert in alertEntity)
                {
                    if (alert.UserAlerts.Count == 0 || alert.UserAlerts.Where(a => a.userID == userID).Count() == 1)
                    alertList.Add(new Alert()
                    {
                        AlertID = alert.Id,
                        Description = alert.description,
                        AlertType = alert.alertType,
                        CreateDate = alert.createDate
                    });
                }
                return alertList;
            }
        }
    }
}