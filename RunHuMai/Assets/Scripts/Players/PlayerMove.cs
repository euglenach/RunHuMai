using System;
using PlayerInput;
using UnityEngine;
using Zenject;
using UniRx;

namespace Players{
    public class PlayerMove : MonoBehaviour{
        [Inject] private IInputProvider input;

        private void Start(){
            input.InputJump()
                 .Subscribe(_ => Debug.Log("ジャンプ"));
            input.InputMove()
                 .Subscribe(_ => Debug.Log("移動"));
        }
    }
}
