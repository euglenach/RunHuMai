using System;
using System.Linq;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace AudioSystem{
    [RequireComponent(typeof(AudioSource))]
    public class MicrophoneInput : MonoBehaviour{
        private AudioSource micAudio;
        float[] spectrum = new float[2048];
        private int separateNum = 900;
        public int SeparateNum => separateNum;

        private readonly Subject<int> voiceInputStream = new Subject<int>();
        public IObservable<int> OnVoiceInput => voiceInputStream;
        
        async void Start(){
            micAudio = GetComponent<AudioSource>();
            var micDevices = Microphone.devices;

            Debug.Log("既定のデバイス:" + micDevices.First());

            micAudio.clip = Microphone.Start(micDevices.First(), true, 1000, 44100);
            await UniTask.WaitWhile(() =>Microphone.GetPosition(micDevices.First()) <= 0);
        
            // micAudio.Play();
        }
        
        private void FixedUpdate(){
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
            if(maxValue < 0.01f){ return; }
        
            var freq = maxIndex * AudioSettings.outputSampleRate / 2 / spectrum.Length;
            Debug.Log(freq);
            voiceInputStream.OnNext(freq);
        }
    }
}
