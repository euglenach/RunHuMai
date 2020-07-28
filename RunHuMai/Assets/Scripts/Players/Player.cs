using System;
using Obstacles;
using UniRx;
using UnityEngine;

namespace Players{
    public class Player : MonoBehaviour{
        [SerializeField] private PlayerStatusSetting setting;
        private PlayerState state;
        public PlayerState State => state;
        public PlayerStatus Status => setting.GetStatus(currentCharacter);
        private readonly Subject<bool> resultStream = new Subject<bool>();
        public IObservable<bool> OnResult => resultStream;
        private Character currentCharacter;
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
            // Debug.Log(character);
            currentCharacter = character;
        }

        public void Death(){
            if(state != PlayerState.Play){ return;}
            state = PlayerState.Death;
            resultStream.OnNext(false);
        }

        public void Clear(){
            if(state != PlayerState.Play){ return;}
            state = PlayerState.Clear;
            resultStream.OnNext(true);
        }
    }
}
