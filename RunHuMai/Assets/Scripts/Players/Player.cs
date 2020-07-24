using System;
using UnityEngine;

namespace Players{
    public class Player : MonoBehaviour{
        private PlayerState state;
        [SerializeField]private PlayerStatus status;
        public PlayerState State => state;
        public PlayerStatus Status => status;

        private void Start(){
            state = PlayerState.Play;
        }
    }
}
