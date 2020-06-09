using Zenject;
using Stack.Models;
using Stack.Services;
using Stack.Controllers;
using UnityEngine;

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
            Container
                .Bind<IPlatformsFactory>()
                .To<PlatformsFactory>()
                .AsSingle();
            Container
                .Bind<IPlatfromPlacerService>()
                .To<PlatformPlacerService>()
                .AsSingle()
                .WithArguments<StartPointsModel>(startPointsModel);
            Container
                .BindInterfacesAndSelfTo<GameController>()
                .AsSingle();
        }
    }
}

