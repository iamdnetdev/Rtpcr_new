namespace RtpcrCustomerApp.Common.Interfaces
{
    public interface IDbSetting
    {
        string Name { get; set; }
        string ConnectionString { get; set; }
        string SchemaName { get; set; }
    }
}
