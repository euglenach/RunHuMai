using System;
using PlayerInput;
using UnityEngine;
using Zenject;
using UniRx;

namespace Players{
    public class PlayerMove : MonoBehaviour{
        [Inject] private IInputProvider input;
        private Player player;
        private Rigidbody2D rb;
        private bool onGround;

        private void Start(){
            player = GetComponentInParent<Player>();
            rb = GetComponent<Rigidbody2D>();
            
            input.InputJump()
                 .Where(_ => player.State != PlayerState.Death)
                 .Where(_ => onGround)
                 .Subscribe(Jump).AddTo(this);
            input.InputMove()
                 .Where(_ => player.State != PlayerState.Death)
                 .Subscribe(Move).AddTo(this);
        }

        private void Move(float power){
            var moveVector = player.Status.MovePower * power * Time.fixedDeltaTime  * Vector2.right;
            rb.velocity = new Vector2(moveVector.x,rb.velocity.y);
        }

        private void Jump(float power){
            // Debug.Log("power"+power);
            power = Mathf.Clamp(power * 10, 0, 1f);
            var jumpPower = power * player.Status.JumpPower * Vector2.up;
            rb.AddForce(jumpPower,ForceMode2D.Impulse);
        }

        private void OnCollisionExit2D(Collision2D other){
            onGround = false;
        }

        private void OnCollisionEnter2D(Collision2D other){
            onGround = true;
        }
    }
}
