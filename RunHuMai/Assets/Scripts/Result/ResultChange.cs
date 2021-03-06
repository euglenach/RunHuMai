using System;
using Systems;
using Suima;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Result{
    public class ResultChange : MonoBehaviour{
        [Inject] private ResultManager resultManager;
        [Inject] private SePlayer sePlayer;
        [Inject] private SoundDatabase sound;
        [SerializeField] private SpriteRenderer render;
        [SerializeField] private Sprite[] win;
        [SerializeField] private Sprite[] lose;
        [SerializeField] private Text text;
        

        private void Start(){
            var currentChar = (int)resultManager.Character;
            sePlayer.PlayOneShot(sound.Result);
            render.sprite = resultManager.IsClear? win[currentChar] : lose[currentChar];
            text.text = resultManager.IsClear? "Clear!!" : "GameOver...";
        }
    }
}
