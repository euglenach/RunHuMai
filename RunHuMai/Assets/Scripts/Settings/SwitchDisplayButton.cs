using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Settings{
    public class SwitchDisplayButton : MonoBehaviour{
        [SerializeField] protected GameObject showObject;
        [SerializeField] protected GameObject hiddenObject;

        protected virtual void Start(){
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => {
                    showObject.SetActive(true);
                    hiddenObject.SetActive(false);
                }).AddTo(this);
        }
    }
}
