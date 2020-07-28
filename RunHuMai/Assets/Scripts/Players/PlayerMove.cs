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
        private PlayerAnimation animation;

        private void Start(){
            player = GetComponentInParent<Player>();
            rb = GetComponent<Rigidbody2D>();
            animation = GetComponentInChildren<PlayerAnimation>();
            
            input.InputJump()
                 .Where(_ => player.State != PlayerState.Death)
                 .Where(_ => onGround)
                 .Subscribe(Jump).AddTo(this);
            input.InputMove()
                 .Where(_ => player.State != PlayerState.Death)
                 .Subscribe(Move).AddTo(this);
        }

        private void Move(float power){
            if(power <= 0){
                animation.StopAnimation();
                return;
            }

            if(onGround)animation.StartAnimation();
            // Debug.Log("power"+power);
            // power = Mathf.Clamp(power, 0.3f, .3f);
            var moveVector = player.Status.MovePower * power * Time.fixedDeltaTime  * Vector2.right;
            if(!onGround){
                moveVector *= .4f;
                
                // if(moveVector.x != 0){Debug.Log(moveVector.x);}
            }
            rb.velocity = new Vector2(moveVector.x,rb.velocity.y);
        }

        private void Jump(float power){
            // Debug.Log("power"+power);
            animation.StartAnimation();
            power = Mathf.Clamp(power, 0f, .3f);
            var jumpPower = power * player.Status.JumpPower * Vector2.up;
            rb.AddForce(jumpPower,ForceMode2D.Impulse);
        }

        private void OnTriggerExit2D(Collider2D other){
            if(other.gameObject.CompareTag("Ground")){
                onGround = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other){
            if(other.gameObject.CompareTag("Ground")){
                onGround = true;
            }
        }

        private void Update(){
            // Debug.Log(onGround);
        }
    }
}
