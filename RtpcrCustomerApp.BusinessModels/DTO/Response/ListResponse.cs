namespace RtpcrCustomerApp.BusinessModels.DTO.Response
{
    using System.Collections.Generic;

    public class ListResponse<T> : List<T>, IResponse where T : IResponse
    {
    }
}
