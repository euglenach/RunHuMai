using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Settings{
    public class VolumeSetting : SettingBase{
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerGroup mixerGroup;
        private Slider slider;

        public override void Init(){
            slider = GetComponentInChildren<Slider>(true);
            SetVolume(DefaultValue);
            slider.value = DefaultValue;

            slider.OnValueChangedAsObservable()
                  .Subscribe(SetVolume)
                  .AddTo(this);
        }
        
        private void SetVolume(float value){
            var decibel = 20.0f * Mathf.Log10(value);
            if(float.IsNegativeInfinity(decibel)){ decibel = -96f; }
            mixer.SetFloat(mixerGroup.name, decibel);
        }
    }
}
