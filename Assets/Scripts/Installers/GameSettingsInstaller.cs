using UnityEngine;
using Zenject;

using Stack.Models;

namespace Stack.Installers
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public CommonPrefabsModel CommonPrefabsModel;
        public CommonSettingsModel CommonSettingsModel;

        public override void InstallBindings()
        {
            Container.BindInstance(CommonPrefabsModel);
            Container.BindInstance(CommonSettingsModel);
        }
    }
}
