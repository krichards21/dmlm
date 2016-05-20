using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class PageModel
    {
        public class Page
        {
            public int ServiceProviderID { get; set; }
            public string Layout { get; set; }
            public string ServiceProviderName { get; set; }
            public List<Widget> Widgets { get; set; }
            public Role Role { get; set; }
        }

        public class Widget
        {
            public string Name { get; set; }
            public int? Priority { get; set; }
            public string Location { get; set; }
            public dmlm.Widget WidgetType { get; set; } 
        }

        public enum Role
        {
            User = 1,
            Manager = 2,
            SuperUser = 3,
            Admin = 4,
            Director = 5,
            Owner = 6
        }

        public enum Widgets
        {
            Agents = 1,
            Location = 2,
            Product = 3,
            Category = 4,
            Reports = 5,
            Groups = 6,
            Regions = 7,
            Maps = 8,
            MapPins = 9
        }

        public Page GetPage(User user)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var page = new Page();
                var serviceProvider = db.ServiceProviders.Find(user.serviceProviderId);
                page.Layout = serviceProvider.layout;
                page.ServiceProviderID = user.serviceProviderId;
                page.ServiceProviderName = serviceProvider.name;
                page.Role = (Role)user.role;

                page.Widgets = new List<Widget>();
                if (serviceProvider.ServiceProviderWidgets.Count > 0)
                {
                    foreach (var widget in serviceProvider.ServiceProviderWidgets.OrderBy(spw => spw.Priority))
                    {
                        if (widget.ServiceProviderWidgetRoles.Where(spw => spw.RoleID == user.role).Count() > 0)
                        {
                            page.Widgets.Add(new Widget
                            {
                                Name = widget.WidgetName,
                                Location = string.Format("~/Views/Widgets/_{0}.cshtml", widget.Widget.WidgetName),
                                Priority = widget.Priority,
                                WidgetType = widget.Widget
                            });
                        }
                    }
                }
                return page;
            }
        }
    }
}