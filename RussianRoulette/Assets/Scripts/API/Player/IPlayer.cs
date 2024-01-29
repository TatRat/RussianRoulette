using UnityEngine;

namespace TatRat.API
{
    public interface IPlayer
    {
        public GameObject GameObject { get; }
        public void Enable();
        public void Disable();
    }
}