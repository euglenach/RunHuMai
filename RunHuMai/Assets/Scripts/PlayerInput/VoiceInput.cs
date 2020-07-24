using System;
using AudioSystem;
using UniRx;
using Zenject;

namespace PlayerInput{
    public class VoiceInput : IInputProvider{
        [Inject] private MicrophoneInput input;
        
        public IObservable<float> InputMove(){
            return input.OnVoiceInput
                        .Where(voice => voice.Value >= voice.SeparateNum)
                        .Select(voice => voice.Volume);
        }

        public IObservable<float> InputJump(){
            return input.OnVoiceInput
                        .Where(voice => voice.Value < voice.SeparateNum)
                        .Select(voice => voice.Volume);
        }
    }
}
