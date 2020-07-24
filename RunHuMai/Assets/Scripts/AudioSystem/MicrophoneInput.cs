using System;
using System.Linq;
using PlayerInput;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace AudioSystem{
    [RequireComponent(typeof(AudioSource))]
    public class MicrophoneInput : MonoBehaviour{
        private AudioSource micAudio;
        float[] spectrum = new float[2048];
        public int SeparateNum{get;set;} = 700;
        private readonly Subject<VoiceStatus> voiceInputStream = new Subject<VoiceStatus>();
        public IObservable<VoiceStatus> OnVoiceInput => voiceInputStream;
        
        void Start(){
            micAudio = GetComponent<AudioSource>();
            var micDevices = Microphone.devices;

            Debug.Log("既定のデバイス:" + micDevices.First());

            StartMicrophone(micDevices.First());
        }
        
        private void FixedUpdate(){
            if(micAudio.clip == null){ return;}
            
            micAudio.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        
            var maxIndex = 0;
            var maxValue = 0.0f;
            for(var i = 0; i < spectrum.Length; i++){
                var val = spectrum[i];
                if(val > maxValue){
                    maxValue = val;
                    maxIndex = i;
                }
            }
            if(maxValue < 0.02f){ maxIndex = 0; }
        
            var freq = maxIndex * AudioSettings.outputSampleRate / 2 / spectrum.Length;
            Debug.Log(freq);
            voiceInputStream.OnNext(new VoiceStatus(freq,SeparateNum,maxValue));
        }

        public void StopMicrophone(){
            micAudio.clip = null;
            micAudio.Stop();
        }
        public async void StartMicrophone(string deviceName = null){
            micAudio.clip = Microphone.Start(deviceName, true, 1000, 44100);
            await UniTask.WaitWhile(() =>Microphone.GetPosition(deviceName) <= 0);
            micAudio.Play();
        }
    }
}
