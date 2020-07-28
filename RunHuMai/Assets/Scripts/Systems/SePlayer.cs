using UnityEngine;

namespace Systems{
    public class SePlayer : MonoBehaviour{
        [SerializeField]private AudioSource audioSource;
        public void PlayOneShot(AudioClip clip) {
            audioSource.PlayOneShot(clip);
        }
        
        public void PlayOneShot(AudioClip clip, float volumeScale) {
            audioSource.PlayOneShot(clip, volumeScale);
        }
    }
}
