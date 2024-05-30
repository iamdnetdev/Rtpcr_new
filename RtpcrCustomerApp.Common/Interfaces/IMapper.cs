namespace RtpcrCustomerApp.Common.Interfaces
{
    public interface IMapper
    {
        U Map<T, U>(T t);
    }
}
