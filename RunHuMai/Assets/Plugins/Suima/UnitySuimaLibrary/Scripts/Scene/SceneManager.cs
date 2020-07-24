using System;
using System.Threading;
using DG.Tweening;
using Suima.Extensions;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Suima.Scene{
    public static class SceneManager{
        private static bool isFade;
        
        public static void Load(Scene nextScene){
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene.ToString());
        }

        public static AsyncOperation LoadAsync(Scene nextScene){
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene.ToString());
        }
        
        public static AsyncOperation LoadAsync(Scene nextScene,LoadSceneParameters parameters){
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene.ToString(),parameters);
        }

        public static Scene NoeScene => (Scene)Enum.Parse(typeof(Scene),UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        public static async UniTaskVoid FadeLoad(Scene nextScene,float duration,CancellationToken cancellationToken = default){
            if(isFade){ Debug.LogWarning("シーン遷移中は新たにシーン遷移できません"); }
            
            isFade = true;
            var ao = LoadAsync(nextScene);
            ao.allowSceneActivation = false;
            
            var fadeCanvas = new GameObject("FadeCanvas").AddComponent<Canvas>();
            var fadeImage = new GameObject("LoadImage").AddComponent<Image>();
            fadeImage.transform.SetParent(fadeCanvas.transform);
            fadeImage.color = Color.black;
            fadeImage.SetAlpha(0);
            fadeImage.GetComponent<RectTransform>().sizeDelta = new Vector2(5000, 5000);
            fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            fadeCanvas.sortingOrder = 999;
            fadeCanvas.gameObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            Object.DontDestroyOnLoad(fadeCanvas.gameObject);

            async UniTask DoFade(float endValue){
                var t = DOTween.ToAlpha(() => fadeImage.color, x => fadeImage.color = x, endValue, duration);
                t.SetTarget(fadeImage);
                await UniTask.Delay(Mathf.FloorToInt(duration * 1000),cancellationToken: cancellationToken);
            }
            await DoFade(1);
            ao.allowSceneActivation = true;
            await ao;
            await DoFade(0);
            Object.Destroy(fadeCanvas.gameObject);
            isFade = false;
        }
    }
}
