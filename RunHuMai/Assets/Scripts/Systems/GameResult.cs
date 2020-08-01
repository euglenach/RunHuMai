using System;
using Players;
using Result;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using SceneManager = Suima.Scene.SceneManager;
using Scene = Suima.Scene.Scene;

namespace Systems{
    public class GameResult : MonoBehaviour{
        [Inject] private Player player;
        [Inject] private BGMPlayer bgmPlayer;
        private void Start(){
            player.OnResult.First()
                  .Subscribe(Over)
                  .AddTo(this);
        }

        private async void Over(bool isClear){
            bgmPlayer.Stop();
            var token = this.GetCancellationTokenOnDestroy();
            var nowScene = SceneManager.NoeScene;
            await UniTask.Delay(2000, cancellationToken : token);
            void OverEvent(UnityEngine.SceneManagement.Scene scene,LoadSceneMode mode){
                var rm = GameObject.Find("ResultManager").GetComponent<ResultManager>();
                rm.Init(isClear,nowScene,player.CurrentCharacter);
                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OverEvent;
            };
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OverEvent;

            SceneManager.FadeLoad(Scene.Result, 1, this.GetCancellationTokenOnDestroy());
        }
    }
}
