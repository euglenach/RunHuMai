using System;
using UnityEngine;

namespace Players{
    [Serializable]
    public struct PlayerStatus{
        [SerializeField] private float jumpPower;
        [SerializeField] private float movePower;
        public float JumpPower => jumpPower;
        public float MovePower => movePower;
    }
}
