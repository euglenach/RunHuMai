using System;
using UnityEngine;

namespace Stages{
    public class MoveFloor : MonoBehaviour{
        private Rigidbody2D rb;
        private void Start(){
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate(){
            var theta = Mathf.Sin(Time.fixedTime / 2);
            rb.velocity = new Vector2(rb.velocity.x,theta * .65f);
        }
    }
}
