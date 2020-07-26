using UnityEngine;

namespace Settings{
    public abstract class SettingBase : MonoBehaviour{
        [SerializeField] private float defaultValue;
        public float DefaultValue => defaultValue;
        public abstract void Init();
    }
}
