using System;
using UniRx;
using UnityEngine;

namespace PlayerInput{
    public class KeyInput : IInputProvider{
        public IObservable<float> InputMove(){
            return Observable.EveryUpdate()
                             .Where(_ => Input.GetKey(KeyCode.RightArrow))
                             .Select(_ => 1f);
        }

        public IObservable<float> InputJump(){
            return Observable.EveryUpdate()
                             .Where(_ => Input.GetKeyDown(KeyCode.Space))
                             .Select(_ => 1f);
        }
    }
}
