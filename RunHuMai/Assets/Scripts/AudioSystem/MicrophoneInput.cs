using System;
using System.Linq;
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
#if UNITY_WEBGL && !UNITY_EDITOR
        void Awake()
        {
            Microphone.Init();
            Microphone.QueryAudioInput();
        }
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        void Update()
        {
            Microphone.Update();
        }
#endif
        
        void Start(){
            micAudio = GetComponent<AudioSource>();
            var micDevices = Microphone.devices;

            Debug.Log("既定のデバイス:" + micDevices.First());

            StartMicrophone(micDevices.First());
        }
        
        private void FixedUpdate(){
            if(micAudio.clip == null){ return;}
#if !(UNITY_WEBGL && !UNITY_EDITOR)
            var data = SoundLibrary.AnalyzeSound(micAudio,2048,0.04f);
#endif

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
            
#if UNITY_WEBGL && !UNITY_EDITOR
            maxValue = Microphone.volumes.Max();
#endif

            voiceInputStream.OnNext(new VoiceStatus(0,maxValue));
            // text.text = Mathf.RoundToInt(data.Pitch).ToString();
            // if(maxValue > 0)Debug.Log(maxValue);
        }

        public void StopMicrophone(){
            micAudio.clip = null;
            micAudio.Stop();
        }
        public async void StartMicrophone(string deviceName = null){
            // micAudio.clip = Microphone.Start(deviceName, true, 1000, 44100);
#if UNITY_WEBGL && !UNITY_EDITOR
            Microphone.Init();
            Microphone.QueryAudioInput();
#else
            micAudio.clip = Microphone.Start(deviceName, true, 1000, 44100);
            await UniTask.WaitWhile(() =>Microphone.GetPosition(deviceName) <= 0);
#endif
            // await UniTask.WaitWhile(() =>Microphone.GetPosition(deviceName) <= 0);
            micAudio.Play();
        }
    }
}
