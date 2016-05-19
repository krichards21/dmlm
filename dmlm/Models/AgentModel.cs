using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class AgentModel
    {
        public class Agents
        {
            public int Id { get; set; }
            public string Fullname { get; set; }
        }

        public List<Agents> GetAgent(PageModel.Page page)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                return db.Users.Where(u => u.serviceProviderId == page.ServiceProviderID).Select(u => new Agents()
                {
                    Id = u.Id, 
                     Fullname = u.firstName + " " + u.lastName
                }).ToList();
            }
        }
    }
}