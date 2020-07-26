using System;
using Players;
using UnityEngine;
using Zenject;

namespace Systems{
    public class CameraMove : MonoBehaviour{
        [Inject] private Player player;
        [SerializeField] private float offset;

        private void Update(){
            var pos = transform.position;
            pos.x = player.transform.position.x + offset;
            transform.position = pos;
        }
    }
}
