using System;
using Obstacles;
using UnityEngine;

namespace Players{
    public class PlayerCollision: MonoBehaviour{
        private Player player;

        private void Start(){
            player = GetComponentInParent<Player>();
        }
        
        private void OnCollisionEnter2D(Collision2D other){
            if(other.gameObject.GetComponent<Obstacle>()){
                player.Death();
            }
        }

        private void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("Goal")){
                player.Clear();
            }
        }
    }
}
