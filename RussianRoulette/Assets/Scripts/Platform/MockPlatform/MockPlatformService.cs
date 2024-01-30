using System;
using TatRat.API;

namespace Platform.MockPlatform
{
    public class MockPlatformService : IPlatformService
    {
        public event Action<ApplicationVisibilityType> ApplicationVisibilityTypeChanged;
    }
}