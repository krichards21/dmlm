using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using dmlm;
using dmlm.Models;
using dmlm.Filter;

namespace dmlm.Controllers
{
    [RoutePrefix("Alerts")]
    public class AlertsController : ApiController
    {
        //private bool _isAuth = false;
        //private int _serviceProviderID = 0; 
        //public void AlertController()
        //{
        //    int.TryParse(Request.Headers.GetValues("ServiceProviderID").First(), out _serviceProviderID);
        //    var accessToken = Request.Headers.GetValues("AccessToken").First();
        //    var userID = Request.Headers.GetValues("UserID").First();
        //    _isAuth = new Models.UserModel().CheckAccessToken(accessToken, userID);
        //}
        private dmlmEntities db = new dmlmEntities();

        //// GET: api/Alerts
        //public IQueryable<Alert> GetAlerts()
        //{
        //    return db.Alerts;
        //}

        // GET: api/Alerts/5
        [ResponseType(typeof(Alert))]
        public IHttpActionResult GetAlert(int id)
        {
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return NotFound();
            }

            return Ok(alert);
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Models.LocationModel.Location))]
        [LoginFilter]
        [Route("GetAlerts/{id:int}")]
        public IHttpActionResult GetListAlerts(int id)
        {
            var serviceProviderID = 0;
            int.TryParse(Request.Headers.GetValues("ServiceProviderID").First(), out serviceProviderID);
            return Ok(new Models.AlertModel().GetAlerts(serviceProviderID, id));
        }

        // PUT: api/Alerts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlert(int id, Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alert.Id)
            {
                return BadRequest();
            }

            db.Entry(alert).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Alerts
        [ResponseType(typeof(Alert))]
        public IHttpActionResult PostAlert(Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alerts.Add(alert);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AlertExists(alert.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = alert.Id }, alert);
        }

        // DELETE: api/Alerts/5
        [ResponseType(typeof(Alert))]
        public IHttpActionResult DeleteAlert(int id)
        {
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return NotFound();
            }

            db.Alerts.Remove(alert);
            db.SaveChanges();

            return Ok(alert);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlertExists(int id)
        {
            return db.Alerts.Count(e => e.Id == id) > 0;
        }
    }
}