using System;
using Players;
using UniRx;
using UnityEngine;

namespace Obstacles{
    public class CollisionEvent : ObstacleEvent{
        private void OnTriggerEnter2D(Collider2D other){
            if(other.GetComponent<PlayerCollision>()){
                invokeStream.OnNext(Unit.Default);
            }
        }
    }
}
