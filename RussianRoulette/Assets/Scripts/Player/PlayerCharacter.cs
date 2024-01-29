using TatRat.API;
using UnityEngine;

namespace TatRat.Player
{
    public class PlayerCharacter : MonoBehaviour, IPlayer
    {
        public GameObject GameObject => gameObject;

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}