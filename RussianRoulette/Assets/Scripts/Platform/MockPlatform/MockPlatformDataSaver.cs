using TatRat.API;

namespace Platform.MockPlatform
{
    public class MockPlatformDataSaver : IPlatformDataSaver
    {
        public bool TryToSave<T>(T data) => 
            true;
    }
}