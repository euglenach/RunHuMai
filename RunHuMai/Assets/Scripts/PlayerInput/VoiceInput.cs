using System;
using AudioSystem;
using UniRx;
using Zenject;

namespace PlayerInput{
    public class VoiceInput : IInputProvider{
        [Inject] private MicrophoneInput input;
        
        public IObservable<float> InputMove(){
            return input.OnVoiceInput
                        .Select(voice => voice.Pitch <= voice.SeparateNum ? voice.Volume : 0);
        }

        public IObservable<float> InputJump(){
            return input.OnVoiceInput
                        .Where(voice =>voice.Pitch > voice.SeparateNum)
                        .Select(voice => voice.Volume);
        }
    }
}
