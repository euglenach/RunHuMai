using System;
using FrostweepGames.Plugins.Native;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title{
    public class MicPermission : MonoBehaviour{
        private readonly Subject<Unit> onclick = new Subject<Unit>();
        public IObservable<Unit> Onclick => onclick;
        private void Start(){
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => {
                    CustomMicrophone.RequestMicrophonePermission();
                    onclick.OnNext(Unit.Default);
                })
                .AddTo(this);
        }
    }
}
