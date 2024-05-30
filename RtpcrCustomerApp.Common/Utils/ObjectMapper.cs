namespace RtpcrCustomerApp.Common.Utils
{
    using Interfaces;
    public class ObjectMapper : AutoMapper.Mapper, IMapper
    {
        public ObjectMapper(AutoMapper.IConfigurationProvider config) : base(config) { }
    }
}
