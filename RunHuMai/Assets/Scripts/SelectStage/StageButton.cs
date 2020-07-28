using System;
using Systems;
using Suima.Scene;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SelectStage{
    public class StageButton : MonoBehaviour{
        [Inject] private SePlayer sePlayer;
        [Inject] private SoundDatabase sound;
        [SerializeField] private Scene scene;
        private Button button;
        private void Start(){
            button = GetComponent<Button>();

            button.OnClickAsObservable()
                  .First()
                  .Subscribe(_ => {
                      SceneManager.FadeLoad(scene, 1, this.GetCancellationTokenOnDestroy());
                      sePlayer.PlayOneShot(sound.OtherButtonClick2);
                  }).AddTo(this);
        }
    }
}
