using System;
using Players;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Systems{
    public class CharaChangeButton : MonoBehaviour{
        [Inject] private Player player;
        [SerializeField] private Character character;
        
        private void Start(){
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => {
                    player.ChangeCharacter(character);
                })
                .AddTo(this);
        }
    }
}
