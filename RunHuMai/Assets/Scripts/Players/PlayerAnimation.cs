using System;
using System.Threading;
using UniRx.Async;
using UnityEngine;

namespace Players{
    public class PlayerAnimation : MonoBehaviour{
        [SerializeField] private PlayerAnimationSet huAnimation;
        [SerializeField] private PlayerAnimationSet maiAnimation;
        private PlayerAnimationSet currentCharacterSet;
        private AnimationState currentAnimation;
        private Sprite currentSprite;
        private bool stopAnimation;
        private SpriteRenderer render;


        private async UniTaskVoid Start(){
            render = GetComponent<SpriteRenderer>(); 
            SwitchCharacter(Character.Mai);
            // SwitchAnimation(AnimationState.Walk);

            var token = this.GetCancellationTokenOnDestroy();
            while(!token.IsCancellationRequested){
                render.sprite = currentCharacterSet.GetSprite(currentAnimation);
                var task = UniTask.DelayFrame(currentCharacterSet.IntervalFrame,PlayerLoopTiming.FixedUpdate,token);
                var task2 = UniTask.WaitWhile(() => stopAnimation, cancellationToken : token);
                await UniTask.WhenAll(task,task2);
            }
        }

        public void SwitchCharacter(Character character){
            switch(character){
                case Character.Hu:
                    currentCharacterSet = huAnimation;
                    break;
                case Character.Mai:
                    currentCharacterSet = maiAnimation;
                    break;
            }
            render.sprite = currentCharacterSet.GetSprite(currentAnimation);
        }
        public void SwitchAnimation(AnimationState state){
            currentAnimation = state;
            stopAnimation = false;
        }

        public void StopAnimation(){
            stopAnimation = true;
        }
        public void StartAnimation(){
            stopAnimation = false;
        }
    }
}
