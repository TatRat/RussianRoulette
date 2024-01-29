using TatRat.API;
using UnityEngine;

namespace TatRat.Player
{
    [CreateAssetMenu(menuName = PlayerConstants.PLAYER_ASSETS_ROOT + "Player Asset")]
    public class PlayerAsset : ScriptableObject
    {
        [SerializeField] [CheckObject] private PlayerCharacter _playerPrefab;
        
        public PlayerCharacter PlayerPrefab => _playerPrefab;
    }
}