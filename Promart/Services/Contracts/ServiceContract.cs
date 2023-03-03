namespace Promart.Services.Contracts
{
    public abstract class ServiceContract<T> where T: class
    {
        public abstract T GetEntity();
    }
}