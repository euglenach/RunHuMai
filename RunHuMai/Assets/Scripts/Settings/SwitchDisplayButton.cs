using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Settings{
    public class SwitchDisplayButton : MonoBehaviour{
        [SerializeField] private GameObject showObject;
        [SerializeField] private GameObject hiddenObject;

        private void Start(){
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => {
                    showObject.SetActive(true);
                    hiddenObject.SetActive(false);
                }).AddTo(this);
        }
    }
}
