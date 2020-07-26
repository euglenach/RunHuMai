using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Settings{
    public class VolumeSetting : MonoBehaviour{
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private float defaultValue;
        private Slider slider;

        public void Init(){
            slider = GetComponentInChildren<Slider>(true);
            SetVolume(defaultValue);
            slider.value = defaultValue;

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
