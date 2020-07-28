using System;
using Systems;
using Suima.Scene;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Title{
    public class SceneLoad : MonoBehaviour{
        [Inject] private SoundDatabase sound;
        [Inject] private SePlayer sePlayer;
        private void Start(){
            InputAsObservable.GetMouseButtonUp(0)
                             .First()
                             .Subscribe(_ => {
                                 sePlayer.PlayOneShot(sound.OtherButtonClick);
                                 SceneManager.FadeLoad(Scene.StageSelect, 1, this.GetCancellationTokenOnDestroy());
                             }).AddTo(this);
        }
    }
}
