using System;
using Suima.Scene;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace Title{
    public class SceneLoad : MonoBehaviour{
        private void Start(){
            InputAsObservable.GetMouseButtonUp(0)
                             .First()
                             .Subscribe(_ => {
                                 SceneManager.FadeLoad(Scene.StageSelect, 1, this.GetCancellationTokenOnDestroy());
                             }).AddTo(this);
        }
    }
}
