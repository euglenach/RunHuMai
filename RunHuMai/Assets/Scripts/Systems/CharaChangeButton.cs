using System;
using Players;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Systems{
    public class CharaChangeButton : MonoBehaviour{
        [Inject] private Player player;
        [Inject] private SePlayer sePlayer;
        [Inject] private SoundDatabase sound;
        [SerializeField] private Character character;
        
        private void Start(){
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => {
                    sePlayer.PlayOneShot(sound.CharaChange);
                    player.ChangeCharacter(character);
                })
                .AddTo(this);
        }
    }
}
