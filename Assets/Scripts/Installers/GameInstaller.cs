using Zenject;
using Stack.Models;
using Stack.Services;
using Stack.Controllers;
using UnityEngine;
using Stack.Signals;

namespace Stack.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        private CommonPrefabsModel commonPrefabsModel;
        [Inject]
        private CommonSettingsModel commonSettingsModel;

        [SerializeField]
        private StartPointsModel startPointsModel;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            InstallServices();
            InstallControllers();
            DeclareAndBindPlatformPlacedSignal();
        }

        private void InstallServices()
        {
            Container
                .Bind<IPlatformsFactory>()
                .To<PlatformsFactory>()
                .AsSingle();
            Container
                .Bind<IPlatfromPlacerService>()
                .To<PlatformPlacerService>()
                .AsSingle()
                .WithArguments<StartPointsModel>(startPointsModel);
        }

        private void InstallControllers()
        {
            Container
                .BindInterfacesAndSelfTo<GameController>()
                .AsSingle();
            Container
                .Bind<InputController>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void DeclareAndBindPlatformPlacedSignal()
        {
            Container.DeclareSignal<PlatformPlacedSignal>();

            Container
                .BindSignal<PlatformPlacedSignal>()
                .ToMethod<GameController>(h => h.OnPlatformPlaced)
                .FromResolve();
        }
    }
}

