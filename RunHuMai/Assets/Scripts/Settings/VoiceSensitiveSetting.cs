using PlayerInput;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Settings{
    public class VoiceSensitiveSetting : MonoBehaviour{
        Slider slider;
        [Inject] private IInputProvider input;

        private void Start(){
            slider = GetComponent<Slider>();

            slider.OnValueChangedAsObservable()
                  .Subscribe(SetValue).AddTo(this);
        }

        private void SetValue(float value){
            if(input is VoiceInput voiceInput){ voiceInput.Sensitive = value; }
        }
    }
}
