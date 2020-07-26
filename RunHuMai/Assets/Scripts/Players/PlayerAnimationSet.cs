using System;
using System.Linq;
using Suima;
using UnityEngine;

namespace Players{
    [Serializable]
    public struct PlayerAnimationSet{
        [SerializeField] private Character character;
        [SerializeField] private Sprite[] walk;
        [SerializeField] private int intervalFrame;
        public int IntervalFrame => intervalFrame;

        public Sprite GetSprite(AnimationState state){
            switch(state){
                case AnimationState.Walk:
                    walk = walk.RightSlide().ToArray();
                    return walk.First();
            }
            return null;
        }
    }

    public enum AnimationState{
        Walk
    }
}
