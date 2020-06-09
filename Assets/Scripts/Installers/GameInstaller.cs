using System.Collections;
using System.Collections.Generic;
using Zenject;
using Stack.Models;
using Stack.Services;
using Stack.Controllers;

namespace Stack.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        private CommonPrefabsModel commonPrefabsModel;

        public override void InstallBindings()
        {
            Container
                .Bind<IPlatformsFactory>()
                .To<PlatformsFactory>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<GameController>()
                .AsSingle();
        }
    }
}

