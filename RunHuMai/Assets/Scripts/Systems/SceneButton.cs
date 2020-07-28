using System;
using Suima.Scene;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Systems{
    public class SceneButton : MonoBehaviour{
        [SerializeField] private bool isReturn;        
        [SerializeField] private Scene scene;
        [Inject] private ResultManager resultManager;

        private void Start(){
            GetComponent<Button>()
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => {
                    if(isReturn){ scene = resultManager.StageScene; }
                    SceneManager.FadeLoad(scene,1,this.GetCancellationTokenOnDestroy());
                })
                .AddTo(this);
        }
    }
}
