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
                 .Where(_ => onGround)
                 .Subscribe(Jump).AddTo(this);
            input.InputMove()
                 .Subscribe(Move).AddTo(this);
        }

        private void Move(float power){
            var moveVector = player.Status.MovePower * power * Vector2.right;
            rb.velocity = new Vector2(moveVector.x,rb.velocity.y);
            // rb.AddForce(moveVector,ForceMode2D.Impulse);
            // if(power == 0){
            //     rb.velocity = new Vector2(0,rb.velocity.y);
            // }
        }

        private void Jump(float power){
            Debug.Log("ジャンプ");
            var jumpPower = player.Status.JumpPower * power * Vector2.up;
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
