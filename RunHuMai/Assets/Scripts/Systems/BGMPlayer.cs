using UnityEngine;

namespace Systems{
    public class BGMPlayer : MonoBehaviour{
        [SerializeField] private AudioSource audioSource;
    
        public void Play(AudioClip clip){
            audioSource.clip = clip;
            audioSource.Play();    
        }

        public void Stop(){
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}
