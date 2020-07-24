using UnityEngine;
using UnityEngine.Audio;

namespace Settings{
    public class VolumeSetting : MonoBehaviour{
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private float defaultValue;

        public void Init(){
            SetVolume(defaultValue);
        }
        
        private void SetVolume(float value){
            var decibel = 20.0f * Mathf.Log10(value);
            if(float.IsNegativeInfinity(decibel)){ decibel = -96f; }
            mixer.SetFloat(mixerGroup.name, decibel);
        }
    }
}
