using System.Linq;
using UnityEngine;

namespace Players{
    [CreateAssetMenu(fileName = "PlayerStatusSetting", menuName = "PlayerStatusSetting", order = 0)]
    public class PlayerStatusSetting : ScriptableObject{
        [SerializeField] private PlayerStatus[] playerStatuses;

        public PlayerStatus GetStatus(Character character){
            return playerStatuses.First(c => c.Character == character);
        }
    }
}
