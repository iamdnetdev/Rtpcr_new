namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using BusinessModels.DBO.InParams.Common;
    using BusinessModels.DBO.OutParams.Common;
    using System;
    using System.Collections.Generic;

    public interface ILocationRepository
    {
        LocationResult GetById(Guid id);
        List<LocationResult> GetAllLocations();
        LocationResult Insert(LocationInsert Location);
        LocationResult Update(LocationUpdate Location);
    }
}
