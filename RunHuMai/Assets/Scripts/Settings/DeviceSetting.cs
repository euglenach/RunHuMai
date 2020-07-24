using System.Linq;
using AudioSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Settings{
    public class DeviceSetting : MonoBehaviour{
        [Inject] private MicrophoneInput microphoneInput;
        private void Start(){
            var dropDown = GetComponent<Dropdown>();
            dropDown.AddOptions(Microphone.devices.ToList());

            dropDown.onValueChanged.AsObservable()
                    .Subscribe(value => {
                        microphoneInput.StopMicrophone();
                        var  deviceName = dropDown.options[value].text;
                        microphoneInput.StartMicrophone(deviceName);
                    }).AddTo(this);
        }
    }
}
