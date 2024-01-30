using TatRat.API;

namespace Platform.MockPlatform
{
    public class MockPlatformDataLoader : IPlatformDataLoader
    {
        public bool TryToLoad<T>(out T data)
        {
            data = default;
            return true;
        }
    }
}