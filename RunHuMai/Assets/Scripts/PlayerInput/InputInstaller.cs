using Zenject;

namespace PlayerInput{
    public class InputInstaller : Installer<InputInstaller>{
        public override void InstallBindings(){
            Container
                .Bind<IInputProvider>()
                .To<KeyInput>()
                .AsCached();
        }
    }
}
