using System;
using AudioSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Stages{
    public class MicResetButton : MonoBehaviour{
        private Button button;
        [Inject] private MicrophoneInput mic;
        private void Start(){
            button = GetComponent<Button>();

            button.OnClickAsObservable()
                  .ThrottleFirst(TimeSpan.FromSeconds(1))
                  .Subscribe(_ => {
                      mic.RestartMicrophone();
                  })
                  .AddTo(this);
        }
    }
}
