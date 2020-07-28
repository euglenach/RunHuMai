using System;
using AudioSystem;
using UniRx;
using Zenject;

namespace PlayerInput{
    public class VoiceInput : IInputProvider{
        [Inject] private MicrophoneInput input;
        public float SeparateNum{get;set;} = 0.3f;
        public float UnderNum{get;set;} = 0.02f;
        
        public IObservable<float> InputMove(){
            return input.OnVoiceInput
                        // .Where(voice =>voice.Volume >= UnderNum)
                        .Select(voice => voice.Volume >= UnderNum ? voice.Volume : 0);
        }

        public IObservable<float> InputJump(){
            return input.OnVoiceInput
                        .Where(voice =>voice.Volume > SeparateNum)
                        .Select(voice => voice.Volume);
        }
    }
}
