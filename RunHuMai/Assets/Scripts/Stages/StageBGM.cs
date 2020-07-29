using System;
using Systems;
using UnityEngine;
using Zenject;

namespace Stages{
    public class StageBGM : MonoBehaviour{
        [Inject] private BGMPlayer bgmPlayer;
        [Inject] private SoundDatabase sound;
        private void Start(){
            bgmPlayer.Play(sound.Stage);
        }
    }
}
