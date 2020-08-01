using System;
using System.Linq;
using FrostweepGames.Plugins.Native;
using PlayerInput;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace AudioSystem{
    [RequireComponent(typeof(AudioSource))]
    public class MicrophoneInput : MonoBehaviour{
        private AudioSource micAudio;
        float[] spectrum = new float[2048];
        private readonly Subject<VoiceStatus> voiceInputStream = new Subject<VoiceStatus>();
        public IObservable<VoiceStatus> OnVoiceInput => voiceInputStream;
        
        void Start(){

            micAudio = GetComponent<AudioSource>();
            var micDevices = CustomMicrophone.devices;

            Debug.Log("既定のデバイス:" + micDevices.First());

            StartMicrophone(micDevices.First());
        }
        
        private void FixedUpdate(){
            if(micAudio.clip == null){ return;}
            
            // var data = SoundLibrary.AnalyzeSound(micAudio,2048,0.04f);


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

            // if(maxValue < 0.02f){ maxValue = 0; }

            // var freq = maxIndex * AudioSettings.outputSampleRate / 2 / spectrum.Length;
            // Debug.Log(freq);

            voiceInputStream.OnNext(new VoiceStatus(0,maxValue));
            // text.text = Mathf.RoundToInt(data.Pitch).ToString();
            // if(maxValue > 0)Debug.Log(maxValue);
        }

        public void StopMicrophone(){
            micAudio.clip = null;
            micAudio.Stop();
        }
        public  void StartMicrophone(string deviceName = null){
            micAudio.clip = Microphone.Start(deviceName, true, 2000, 44100);
            // micAudio.clip = CustomMicrophone.Start(null, true, 1000, 44100);
            // await UniTask.WaitWhile(() =>CustomMicrophone.GetPosition(deviceName) <= 0);
            // await UniTask.WaitWhile(() =>Microphone.GetPosition(deviceName) <= 0);
            micAudio.Play();
        }
    }
}
