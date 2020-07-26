using System;
using UniRx;
using UnityEngine;

namespace Players{
    public class Player : MonoBehaviour{
        private PlayerState state;
        [SerializeField]private PlayerStatus status;
        public PlayerState State => state;
        public PlayerStatus Status => status;
        private readonly Subject<Unit> deathStream = new Subject<Unit>();
        public IObservable<Unit> OnDeath => deathStream;

        private void Start(){
            state = PlayerState.Play;
        }

        private void OnTriggerEnter2D(Collider2D other){
            state = PlayerState.Death;
        }
    }
}
