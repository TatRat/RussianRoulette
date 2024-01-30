using System;

namespace TatRat.API
{
    public interface IPlatformService : IService
    {
        public event Action<ApplicationVisibilityType> ApplicationVisibilityTypeChanged;
    }
}