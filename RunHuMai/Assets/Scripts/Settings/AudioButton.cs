using System;
using Systems;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Settings{
    public class AudioButton : MonoBehaviour{
        [SerializeField] private AudioClip clip;
        [Inject] private SePlayer sePlayer;
        
        private void Start(){
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => {
                    sePlayer.PlayOneShot(clip);
                })
                .AddTo(this);
        }
    }
}
