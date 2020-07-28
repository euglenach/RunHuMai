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
        private readonly Subject<Unit> deathStream = new Subject<Unit>();
        public IObservable<Unit> OnDeath => deathStream;
        private Character currentCharacter;
        private PlayerAnimation animation;

        private void Start(){
            state = PlayerState.Play;
            animation = GetComponentInChildren<PlayerAnimation>();
            ChangeCharacter(Character.Mai);
        }

        private void Update(){
            if(Input.GetKeyDown(KeyCode.M)) ChangeCharacter(Character.Mai);
            if(Input.GetKeyDown(KeyCode.H)) ChangeCharacter(Character.Hu);
        }

        public void ChangeCharacter(Character character){
            animation.SwitchCharacter(character);
            // Debug.Log(character);
            currentCharacter = character;
        }

        public void Death(){
            state = PlayerState.Death;
            deathStream.OnNext(Unit.Default);
        }
    }
}
