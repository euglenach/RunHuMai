using System;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace Obstacles{
    public class OnigiriFactory : MonoBehaviour{
        [SerializeField] private ObstacleEvent collision;
        [SerializeField] private Onigiri onigiri;
        [SerializeField] private bool loop;
        [SerializeField] private int interval;
        

        private void Start(){
            collision.Invoke.First()
                     .Subscribe(async _ => {
                         var token = this.GetCancellationTokenOnDestroy();
                         while(!token.IsCancellationRequested){
                             var obj = Instantiate(onigiri);
                             obj.transform.position = transform.position;
                             if(!loop){ return; }
                             
                             await UniTask.Delay(interval * 1000,cancellationToken: token);
                         }
                     })
                     .AddTo(this);
        }
    }
}
