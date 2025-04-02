using Zenject;

namespace Jam.Scripts.Utils
{
    public class CoroutineHelperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CoroutineHelper>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("CoroutineHelper")
                .AsSingle()
                .NonLazy();
        }
    }
}
