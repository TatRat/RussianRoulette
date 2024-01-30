using System;

namespace TatRat.API
{
    public interface IAdvertisementService : IService
    {
        public event Action<AdvertisementType> AdvertisementPlayed;
        
        public bool TryToPlayRewardedAdvertisement();
        public bool TryToPlayBannerAdvertisement();
    }
}