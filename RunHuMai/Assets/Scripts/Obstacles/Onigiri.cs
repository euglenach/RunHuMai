using System;
using UnityEngine;

namespace Obstacles{
    public class Onigiri : Obstacle{
        [SerializeField] private float speed;
        private Rigidbody2D rb;
        private void Start(){
            rb = GetComponent<Rigidbody2D>();
            
            // rb.velocity = new Vector2(-speed,0);
        }

        private void Update(){
            rb.velocity = new Vector2(-speed,rb.velocity.y);
        }
    }
}
