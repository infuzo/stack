using Stack.Services;
using Stack.Models;
using Stack.EntitiesBehaviour;

using UnityEngine;

using Zenject;

namespace Stack.Controllers
{
    public class GameController : IInitializable
    {
        protected readonly IPlatformsFactory platformsFactory;
        protected readonly IPlatfromPlacerService platfromPlacerService;
        protected readonly CommonSettingsModel commonSettingsModel;

        protected Platform currentPlatform;

        public GameController(
            IPlatformsFactory platformsFactory,
            IPlatfromPlacerService platfromPlacerService,
            CommonSettingsModel commonSettingsModel)
        {
            this.platformsFactory = platformsFactory;
            this.commonSettingsModel = commonSettingsModel;
            this.platfromPlacerService = platfromPlacerService;
        }

        public void Initialize()
        {
            CreateFirstsPlatforms();
            CreateNewPlatformByCurrent();
        }

        protected virtual void CreateFirstsPlatforms()
        {
            currentPlatform = platformsFactory
                .CreatePlatform(commonSettingsModel.FirstPlatformPosition, commonSettingsModel.FirstPlatformScale);
        }

        protected virtual void CreateNewPlatformByCurrent()
        {
            Vector3 position, direction;
            float maxDistance;
            platfromPlacerService.GetRandomStartPoint(currentPlatform, out position, out direction, out maxDistance);
            currentPlatform = platformsFactory.CreatePlatform(position, currentPlatform.transform.localScale);
            currentPlatform.StartMovement(
                direction, 
                position, 
                commonSettingsModel.PlatformStartSpeed,
                maxDistance);
        }

        public virtual void OnPlatformPlaced()
        {
            currentPlatform.StopMovement();
        }
    }
}

