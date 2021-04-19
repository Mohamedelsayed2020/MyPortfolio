namespace Core.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepositry<T> Entity { get; }
        void Save();
    }
}
