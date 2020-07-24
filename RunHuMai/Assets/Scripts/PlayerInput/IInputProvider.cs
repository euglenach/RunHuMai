using System;
using UniRx;

namespace PlayerInput{
    public interface IInputProvider{
        IObservable<float> InputMove();
        IObservable<float> InputJump();
    }
}
