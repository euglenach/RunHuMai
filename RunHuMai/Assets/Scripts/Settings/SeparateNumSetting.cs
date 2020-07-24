using System;
using System.Linq;
using AudioSystem;
using Suima;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Settings{
    public class SeparateNumSetting : MonoBehaviour{
        [Inject] private MicrophoneInput input;
        
        async UniTask SettingSep(){
            var list = await input.OnVoiceInput.Buffer(TimeSpan.FromSeconds(5)).First();
            list.OrderBy(n => n.Value);
            list.Select(n => n.Value).Print();
            var sep = list[list.Count / 2].Value;
            input.SeparateNum = sep;
        }
    }
}
