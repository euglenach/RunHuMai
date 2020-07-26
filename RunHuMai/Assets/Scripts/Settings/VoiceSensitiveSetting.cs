using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Settings{
    public class VoiceSensitiveSetting : MonoBehaviour{
        [SerializeField] private Slider slider;
        private float voiceSensitive;
        public float VoiceSensitive => voiceSensitive;

        private void Start(){
            slider = GetComponent<Slider>();

            slider.OnValueChangedAsObservable()
                  .Subscribe(SetValue).AddTo(this);
        }

        private void SetValue(float value){
            slider.value = value;
            voiceSensitive = value;
        }
    }
}
