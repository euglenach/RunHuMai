using System;
using System.Linq;
using System.Threading;
using AudioSystem;
using Suima;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Settings{
    public class SeparateNumSetting : MonoBehaviour{
        [Inject] private MicrophoneInput input;
        [SerializeField] private Text text;
        private CancellationTokenSource cts;

        private void OnDisable(){
            cts.Cancel();
        }

        private void OnEnable(){
            cts = new CancellationTokenSource();
            var token = cts.Token;
            
            text.text = "５秒間一定の声を出し続けてください";

            input.OnVoiceInput.First()
                 .Subscribe(async _=> {
                     text.text = "計測中...";
                     await SettingSep(token);
                     text.text = "計測終了！";
                 }).AddTo(this);
        }

        async UniTask SettingSep(CancellationToken token){
            var list = await input.OnVoiceInput.Buffer(TimeSpan.FromSeconds(5)).First().ToUniTask(token);
            list.OrderBy(n => n.Value);
            list.Select(n => n.Value).Print();
            var sep = list[list.Count / 2].Value;
            token.ThrowIfCancellationRequested();
            input.SeparateNum = sep;
        }
    }
}
