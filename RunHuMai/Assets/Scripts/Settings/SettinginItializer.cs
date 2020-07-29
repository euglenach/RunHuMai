using System;
using UnityEngine;

namespace Settings{
    public class SettingInitializer : SingletonMonoBehaviour<SettingInitializer>{
        protected override void Awake(){
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        private void Start(){
            var items = GetComponentsInChildren<VolumeSetting>(true);
            foreach(var item in items){
                item.Init();
            }
        }
    }
}
