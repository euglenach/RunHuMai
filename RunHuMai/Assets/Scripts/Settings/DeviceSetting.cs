using System.Linq;
using AudioSystem;
using FrostweepGames.Plugins.Native;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Settings{
    public class DeviceSetting : MonoBehaviour{
        [Inject] private MicrophoneInput microphoneInput;
        private void Start(){
            var dropDown = GetComponentInChildren<Dropdown>();
            dropDown.AddOptions(CustomMicrophone.devices.ToList());

            dropDown.onValueChanged.AsObservable()
                    .Subscribe(value => {
                        microphoneInput.StopMicrophone();
                        var  deviceName = dropDown.options[value].text;
                        // Debug.Log(deviceName);
                        microphoneInput.StartMicrophone(deviceName);
                    }).AddTo(this);
        }
    }
}
