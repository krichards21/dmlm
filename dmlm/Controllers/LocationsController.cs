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

namespace dmlm.Controllers
{
    [RoutePrefix("Location")]
    public class LocationsController : ApiController
    {
        private Models.LocationModel locationModel = new Models.LocationModel();
        private Models.InventoryLocationsModel InventoryLocationModel = new Models.InventoryLocationsModel();
        private dmlmEntities db = new dmlmEntities();

        // GET: api/Locations
        [ResponseType(typeof(Models.LocationModel.Location))]
        [Route("GetLocations")]
        public IHttpActionResult GetLocations()
        {
            return Ok(locationModel.GetLocations(1));
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Models.LocationModel.Location))]
        [Route("GetLocation/{id:int}")]
        public IHttpActionResult GetLocation(int id)
        {
            var location = locationModel.GetLocation(1, id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Models.LocationModel.Location))]
        [Route("GetLocation/{id:int}/{regionid:int}")]
        public IHttpActionResult GetLocationByRegion(int id, int regionid)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // GET: /Location/GetInventoryCount
        [ResponseType(typeof(LocationProductCounts))]
        [Route("GetInventoryCount/{id:int}/{locationid:int}")]
        public IHttpActionResult GetInventoryCount(int id, int locationid)
        {
            var locationCounts = 
                InventoryLocationModel.GetInventoryCountsByLocation(locationid);
            if (locationCounts == null)
            {
                return NotFound();
            }

            return Ok(locationCounts);
        }

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(location);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LocationExists(location.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocation(int id)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
            db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationExists(int id)
        {
            return db.Locations.Count(e => e.Id == id) > 0;
        }
    }
}