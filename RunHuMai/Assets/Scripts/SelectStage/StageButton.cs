using System;
using Suima.Scene;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace SelectStage{
    public class StageButton : MonoBehaviour{
        [SerializeField] private Scene scene;
        private Button button;
        private void Start(){
            button = GetComponent<Button>();

            button.OnClickAsObservable()
                  .First()
                  .Subscribe(_ => {
                      SceneManager.FadeLoad(scene, 1, this.GetCancellationTokenOnDestroy());
                  }).AddTo(this);
        }
    }
}
