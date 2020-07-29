using System;
using Systems;
using FrostweepGames.Plugins.Native;
using Suima.Scene;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Title{
    [DefaultExecutionOrder(-10)]
    public class SceneLoad : MonoBehaviour{
        [Inject] private SoundDatabase sound;
        [Inject] private SePlayer sePlayer;
        [Inject] private BGMPlayer bgmPlayer;
        [SerializeField] private MicPermission micPermission;
        
        private void Start(){
            bgmPlayer.Play(sound.Title);
            
            micPermission.Onclick
                         .First()
                         .Subscribe(_ => {
                             InputAsObservable.GetMouseButtonUp(0)
                                              .First()
                                              .Subscribe(async __ => {
                                                  CustomMicrophone.RequestMicrophonePermission();
                                                  sePlayer.PlayOneShot(sound.OtherButtonClick);
                                                  // await UniTask.WaitWhile(
                                                  //     () => CustomMicrophone.HasConnectedMicrophoneDevices()
                                                  //           && CustomMicrophone.HasMicrophonePermission());
                                                  
                                                  SceneManager.FadeLoad(Scene.StageSelect, 1, this.GetCancellationTokenOnDestroy());
                                                  
                                              }).AddTo(this);
                         }).AddTo(this);
        }
    }
}
