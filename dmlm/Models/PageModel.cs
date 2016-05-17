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
            public string Layout { get; set; }
            public Dictionary<int, Widget> Widgets { get; set; }
            public Role Role { get; set; }
        }

        public class Widget
        {
            public string Name { get; }
            public int Priority { get; set; }
            public string Location { get; set; }
            public List<Role> Roles { get; set; }
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
            Settings = 7
        }

        public Page GetPage(int serviceProviderID, User user)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var page = new Page();
                var serviceProvider = db.ServiceProviders.Find(serviceProviderID);
                page.Layout = serviceProvider.layout;
            }
            return new Page();
        }
    }
}