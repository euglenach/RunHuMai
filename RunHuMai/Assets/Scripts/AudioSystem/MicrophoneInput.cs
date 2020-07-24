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
        private int separateNum = 500;
        private readonly Subject<VoiceStatus> voiceInputStream = new Subject<VoiceStatus>();
        public IObservable<VoiceStatus> OnVoiceInput => voiceInputStream;
        
        void Start(){
            micAudio = GetComponent<AudioSource>();
            var micDevices = Microphone.devices;

            Debug.Log("既定のデバイス:" + micDevices.First());

            StartMicrophone(micDevices.First(),true);
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
            if(maxValue < 0.01f){ maxIndex = 0; }
        
            var freq = maxIndex * AudioSettings.outputSampleRate / 2 / spectrum.Length;
            Debug.Log(freq);
            voiceInputStream.OnNext(new VoiceStatus(freq,separateNum,maxValue));
        }

        public void StopMicrophone(){
            micAudio.clip = null;
            micAudio.Stop();
        }
        public async void StartMicrophone(string deviceName = null, bool playAudio = false){
            micAudio.clip = Microphone.Start(deviceName, true, 1000, 44100);
            await UniTask.WaitWhile(() =>Microphone.GetPosition(deviceName) <= 0);
            if(!playAudio){ micAudio.volume = 0; }
            micAudio.Play();
        }
    }
}
