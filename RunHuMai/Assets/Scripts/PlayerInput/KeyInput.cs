using System;
using UniRx;
using UnityEngine;

namespace PlayerInput{
    public class KeyInput : IInputProvider{
        public IObservable<Unit> InputMove(){
            return Observable.EveryUpdate()
                             .Where(_ => Input.GetKey(KeyCode.RightArrow))
                             .AsUnitObservable();
        }

        public IObservable<Unit> InputJump(){
            return Observable.EveryUpdate()
                             .Where(_ => Input.GetKeyDown(KeyCode.Space))
                             .AsUnitObservable();
        }
    }
}
