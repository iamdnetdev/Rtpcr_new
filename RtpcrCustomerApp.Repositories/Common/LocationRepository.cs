namespace RtpcrCustomerApp.Repositories.Common
{
    using BusinessModels.DBO.InParams.Common;
    using BusinessModels.DBO.OutParams.Common;
    using Repositories.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class LocationRepository : RepositoryBase, ILocationRepository
    {
        public LocationRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<LocationRepository>())
        {

        }

        public LocationResult GetById(Guid id)
        {
            var location = Query<LocationResult>(Queries.Location.GetLocation,
                            CommandType.StoredProcedure,
                            new KeyValuePair<string, object>("UserID", id)).FirstOrDefault();
            return location;
        }

        public List<LocationResult> GetAllLocations()
        {
            var locations = Query<LocationResult>(Queries.Location.GetLocation,
                            CommandType.StoredProcedure).ToList();
            return locations;
        }

        public LocationResult Insert(LocationInsert location)
        {
            ExecuteCommand(Queries.Location.InsertLocation, location);
            return GetById(location.LocationId);
        }

        public LocationResult Update(LocationUpdate location)
        {
            ExecuteCommand(Queries.Location.UpdateLocation, location);
            return GetById(location.LocationId);
        }
    }
}
