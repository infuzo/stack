using Stack.Services;
using Stack.Models;

using Zenject;

namespace Stack.Controllers
{
    public class GameController : IInitializable
    {
        private readonly IPlatformsFactory platformsFactory;
        private readonly CommonSettingsModel commonSettingsModel;

        public GameController(
            IPlatformsFactory platformsFactory,
            CommonSettingsModel commonSettingsModel)
        {
            this.platformsFactory = platformsFactory;
            this.commonSettingsModel = commonSettingsModel;
        }

        public void Initialize()
        {
            CreateFirstPlatform();
        }

        protected virtual void CreateFirstPlatform()
        {
            platformsFactory.CreatePlatform(commonSettingsModel.FirstPlatformPosition, commonSettingsModel.FirstPlatformScale);
        }
    }
}

