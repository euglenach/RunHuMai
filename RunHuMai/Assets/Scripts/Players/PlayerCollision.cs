using System;
using Obstacles;
using UnityEngine;

namespace Players{
    public class PlayerCollision: MonoBehaviour{
        private Player player;

        private void Start(){
            player = GetComponentInParent<Player>();
            
            
        }
        
        private void OnTriggerEnter2D(Collider2D other){
            if(!other.GetComponent<Obstacle>()){ return;}

            player.Death();
        }
    }
}
