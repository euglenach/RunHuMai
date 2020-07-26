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


        private async UniTaskVoid Start(){
            var render = GetComponent<SpriteRenderer>();

            var token = this.GetCancellationTokenOnDestroy();
            while(!token.IsCancellationRequested){
                render.sprite = currentCharacterSet.GetSprite(currentAnimation);
                var task = UniTask.DelayFrame(currentCharacterSet.IntervalFrame,cancellationToken : token);
                var task2 = UniTask.WaitWhile(() => stopAnimation, cancellationToken : token);
                await UniTask.WhenAll(task,task2);
            }
        }

        public void SwitchCharacter(Character character,Action action = null){
            switch(character){
                case Character.Hu:
                    currentCharacterSet = huAnimation;
                    break;
                case Character.Mai:
                    currentCharacterSet = maiAnimation;
                    break;
            }
            action?.Invoke();
        }
        public void SwitchAnimation(AnimationState state,Action action = null){
            currentAnimation = state;
            stopAnimation = false;
            action?.Invoke();
        }

        public void StopAnimation(){
            stopAnimation = true;
        }
        public void StartAnimation(){
            stopAnimation = false;
        }
    }
}
