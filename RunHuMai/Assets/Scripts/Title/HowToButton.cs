using System;
using Settings;
using Suima;
using UniRx.Async;
using UnityEngine;

namespace Title{
    public class HowToButton : SwitchDisplayButton{
        protected override void Start(){
            if(hiddenObject == null) hiddenObject = SettingInitializer.Instance.gameObject;
            if(showObject == null) showObject = SettingInitializer.Instance.gameObject;
            Debug.Log(hiddenObject);
            Debug.Log(showObject);
            base.Start();
        }

        
    }
}
