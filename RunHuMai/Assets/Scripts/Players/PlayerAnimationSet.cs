using System;
using System.Linq;
using Suima;
using UnityEngine;

namespace Players{
    [Serializable]
    public class PlayerAnimationSet{
        [SerializeField] private Sprite[] walk;
        [SerializeField] private Sprite[] win;
        [SerializeField] private Sprite[] lose;
        [SerializeField] private int intervalFrame;
        public int IntervalFrame => intervalFrame;

        public Sprite GetSprite(AnimationState state){
            switch(state){
                case AnimationState.Walk:
                    walk = walk.RightSlide().ToArray();
                    return walk.First();
                case AnimationState.Win:
                    win = win.RightSlide().ToArray();
                    return win.First();
                case AnimationState.Lose:
                    lose = lose.RightSlide().ToArray();
                    return lose.First();
            }
            return null;
        }
    }

    public enum AnimationState{
        Walk,Win,Lose
    }
}
