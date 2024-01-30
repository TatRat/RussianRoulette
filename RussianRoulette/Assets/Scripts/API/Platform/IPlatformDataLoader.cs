namespace TatRat.API
{
    public interface IPlatformDataLoader
    {
        public bool TryToLoad<T>(out T data);
    }
}