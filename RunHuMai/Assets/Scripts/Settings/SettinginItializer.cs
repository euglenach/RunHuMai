using System;
using UnityEngine;

namespace Settings{
    public class SettingInitializer : MonoBehaviour{
        private void Start(){
            var items = GetComponentsInChildren<VolumeSetting>();
            foreach(var item in items){
                item.Init();
            }
        }
    }
}
