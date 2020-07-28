using System;
using UniRx;
using UnityEngine;

namespace Obstacles{
    public class OnigiriFactory : MonoBehaviour{
        [SerializeField] private ObstacleEvent collision;

        private void Start(){
            collision.Invoke.First()
                     .Subscribe(_ => {
                         
                     })
                     .AddTo(this);
        }
    }
}
