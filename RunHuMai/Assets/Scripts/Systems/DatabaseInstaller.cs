using UnityEngine;
using Zenject;

namespace Systems{
    [CreateAssetMenu(fileName = "DatabaseInstaller", menuName = "Installers/DatabaseInstaller")]
    public class DatabaseInstaller : ScriptableObjectInstaller<DatabaseInstaller>{
        [SerializeField] private SoundDatabase soundDatabase;

        public override void InstallBindings(){
            Container.BindInstance(soundDatabase).IfNotBound();
        }
    }
}
