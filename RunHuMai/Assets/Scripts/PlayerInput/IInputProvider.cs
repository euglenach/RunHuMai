using System;
using UniRx;

namespace PlayerInput{
    public interface IInputProvider{
        IObservable<Unit> InputMove();
        IObservable<Unit> InputJump();
    }
}
