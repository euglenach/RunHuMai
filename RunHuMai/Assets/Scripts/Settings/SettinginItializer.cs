using System;
using UnityEngine;

namespace Settings{
    public class SettingInitializer : SingletonMonoBehaviour<SettingInitializer>{
        private void Start(){
            var items = GetComponentsInChildren<VolumeSetting>(true);
            foreach(var item in items){
                item.Init();
            }
        }
    }
}
