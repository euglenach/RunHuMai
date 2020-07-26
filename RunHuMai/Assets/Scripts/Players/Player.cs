using System;
using Obstacles;
using UniRx;
using UnityEngine;

namespace Players{
    public class Player : MonoBehaviour{
        [SerializeField] private PlayerStatusSetting setting;
        private PlayerState state;
        public PlayerState State => state;
        private Character currentCharacter;
        public PlayerStatus Status => setting.GetStatus(currentCharacter);
        private readonly Subject<Unit> deathStream = new Subject<Unit>();
        public IObservable<Unit> OnDeath => deathStream;

        private void Start(){
            state = PlayerState.Play;
        }

        private void OnTriggerEnter2D(Collider2D other){
            if(!other.GetComponent<Obstacle>()){ return;}
            
            state = PlayerState.Death;
        }
    }
}
