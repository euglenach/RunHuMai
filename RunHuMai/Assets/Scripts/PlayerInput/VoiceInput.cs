using System;
using AudioSystem;
using UniRx;
using Zenject;

namespace PlayerInput{
    public class VoiceInput : IInputProvider{
        [Inject] private MicrophoneInput input;
        
        public IObservable<Unit> InputMove(){
            return input.OnVoiceInput
                 .Where(freq => freq >= input.SeparateNum)
                 .AsUnitObservable();
        }

        public IObservable<Unit> InputJump(){
            return input.OnVoiceInput
                        .Where(freq => freq < input.SeparateNum)
                        .AsUnitObservable();
        }
    }
}
