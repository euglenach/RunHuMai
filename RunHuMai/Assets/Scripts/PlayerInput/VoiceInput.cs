using System;
using AudioSystem;
using UniRx;
using Zenject;

namespace PlayerInput{
    public class VoiceInput : IInputProvider{
        [Inject] private MicrophoneInput input;
        public float SeparateNum{get;set;} = 0.2f;
        public float UnderNum{get;set;} = 0.02f;
        public float Sensitive{get;set;} = 1;
        
        public IObservable<float> InputMove(){
            return input.OnVoiceInput
                        // .Where(voice =>voice.Volume >= UnderNum)
                        .Select(voice => voice.Volume * Sensitive >= UnderNum ? voice.Volume * Sensitive : 0);
        }

        public IObservable<float> InputJump(){
            return input.OnVoiceInput
                        .Where(voice =>voice.Volume * Sensitive > SeparateNum)
                        .Select(voice => voice.Volume * Sensitive);
        }
    }
}
