using System;
using UniRx;
using UnityEngine;

namespace Obstacles{
    public abstract class ObstacleEvent : MonoBehaviour{
        protected readonly Subject<Unit> invokeStream = new Subject<Unit>();
        public IObservable<Unit> Invoke => invokeStream;
    }
}
