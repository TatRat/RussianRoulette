namespace TatRat.API
{
    public interface IPlatformDataSaver
    {
        public bool TryToSave<T>(T data);
    }
}