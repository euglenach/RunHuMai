using System;
using Systems;
using Obstacles;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players{
    public class Player : MonoBehaviour{
        [SerializeField] private PlayerStatusSetting setting;
        [Inject] private SePlayer sePlayer;
        [Inject] private SoundDatabase sound;
        private PlayerState state;
        public PlayerState State => state;
        public PlayerStatus Status => setting.GetStatus(currentCharacter);
        private readonly Subject<bool> resultStream = new Subject<bool>();
        public IObservable<bool> OnResult => resultStream;
        private Character currentCharacter;

        public Character CurrentCharacter => currentCharacter;

        private PlayerAnimation animation;

        private void Start(){
            state = PlayerState.Play;
            animation = GetComponentInChildren<PlayerAnimation>();
            ChangeCharacter(Character.Mai);
        }

#if UNITY_EDITOR
        private void Update(){
            if(Input.GetKeyDown(KeyCode.M)) ChangeCharacter(Character.Mai);
            if(Input.GetKeyDown(KeyCode.H)) ChangeCharacter(Character.Hu);
        }
#endif
        public void ChangeCharacter(Character character){
            animation.SwitchCharacter(character);
            animation.StartAnimation();
            // Debug.Log(character);
            currentCharacter = character;
        }

        public void Death(){
            if(state != PlayerState.Play){ return;}
            state = PlayerState.Death;
            animation.SwitchAnimation(AnimationState.Lose);
            sePlayer.PlayOneShot(sound.GameOver);
            resultStream.OnNext(false);
        }

        public void Clear(){
            if(state != PlayerState.Play){ return;}
            state = PlayerState.Clear;
            animation.SwitchAnimation(AnimationState.Win);
            sePlayer.PlayOneShot(sound.GameClear);
            resultStream.OnNext(true);
        }
    }
}
