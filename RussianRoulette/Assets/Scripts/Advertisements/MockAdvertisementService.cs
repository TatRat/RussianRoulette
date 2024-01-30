using System;
using TatRat.API;

namespace TatRat.Advertisements
{
    public class MockAdvertisementService : IAdvertisementService
    {
        public event Action<AdvertisementType> AdvertisementPlayed;
        
        public bool TryToPlayRewardedAdvertisement() => 
            true;

        public bool TryToPlayBannerAdvertisement() => 
            true;
    }
}