using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class LocationModel
    {
        public class Location
        {
            public int LocationID { get; set; }
            public string LocationName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string County { get; set; }
            public string DaysOpen { get; set; }
            public string HourStart { get; set; }
            public string HourEnd { get; set; }
            public string GoogleMapSmall { get; set; }
            public string GoogleMapMedium { get; set; }
            public string ImageURL { get; set; }
            public string Phone { get; set; }
        }

        public Location GetLocation(int serviceProviderID, int locationID)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var locationEntity = db.Locations.Where(l => l.serviceProviderId == serviceProviderID
                && l.Id == locationID).FirstOrDefault();
                var location = new Location()
                {
                    LocationID = locationEntity.Id,
                    Address1 = locationEntity.address1,
                    Address2 = locationEntity.address2,
                    Address3 = locationEntity.address3,
                    City = locationEntity.city,
                    State = locationEntity.state,
                    GoogleMapSmall = locationEntity.googleMapSmall,
                    GoogleMapMedium = locationEntity.googleMapMedium,
                    HourStart = locationEntity.operatingHoursStart.HasValue ? locationEntity.operatingHoursStart.Value.ToString(@"hh\:mm") : "",
                    HourEnd = locationEntity.operatingHoursEnd.HasValue ? locationEntity.operatingHoursEnd.Value.ToString(@"hh\:mm") : "",
                    DaysOpen = locationEntity.operatingDays,
                    LocationName = locationEntity.name,
                    Phone = locationEntity.phone

                };
                return location;
            }
        }


        public List<Location> GetLocations(int serviceProviderID)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var locationEntity = db.Locations.Where(l => l.serviceProviderId ==
                serviceProviderID);
                var locations = new List<Location>();
                foreach (var location in locationEntity)
                {
                    locations.Add(
                        new Location()
                        {
                            LocationID = location.Id,
                            Address1 = location.address1,
                            Address2 = location.address2,
                            Address3 = location.address3,
                            City = location.city,
                            State = location.state,
                            PostalCode = location.postalCode,
                            GoogleMapSmall = location.googleMapSmall,
                            GoogleMapMedium = location.googleMapMedium,
                            HourStart = location.operatingHoursStart.HasValue ? location.operatingHoursStart.Value.ToString(@"hh\:mm") : "",
                            HourEnd = location.operatingHoursEnd.HasValue ? location.operatingHoursEnd.Value.ToString(@"hh\:mm") : "",
                            DaysOpen = location.operatingDays,
                            LocationName = location.name,
                            ImageURL = location.imageUrl,
                            Phone = location.phone
                        });
                }


                return locations;
            }
        }

        public List<Location> GetLocations(int serviceProviderID, int regionID)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var locationEntity = db.Locations.Where(l => l.serviceProviderId == 
                serviceProviderID);
                var locations = new List<Location>();
                foreach (var location in locationEntity)
                {
                    if (location.RegionLocations.Where(rl => rl.regionId == regionID).Any())
                    {
                        locations.Add(
                            new Location()
                            {
                                LocationID = location.Id,
                                Address1 = location.address1,
                                Address2 = location.address2,
                                Address3 = location.address3,
                                City = location.city,
                                State = location.state,
                                PostalCode = location.postalCode,
                                GoogleMapSmall = location.googleMapSmall,
                                GoogleMapMedium = location.googleMapMedium,
                                HourStart = location.operatingHoursStart.HasValue ? location.operatingHoursStart.Value.ToString(@"hh\:mm") : "",
                                HourEnd = location.operatingHoursEnd.HasValue ? location.operatingHoursEnd.Value.ToString(@"hh\:mm") : "",
                                DaysOpen = location.operatingDays,
                                LocationName = location.name,
                                ImageURL = location.imageUrl,
                                Phone = location.phone
                            });
                    }
                }
                

                return locations;
            }
        }
    }
}