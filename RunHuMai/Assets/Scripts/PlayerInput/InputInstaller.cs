using Zenject;

namespace PlayerInput{
    public class InputInstaller : MonoInstaller{
        public override void InstallBindings(){
            Container
                .Bind<IInputProvider>()
                .To<KeyInput>()
                .AsCached();
        }
    }
}
