using Stack.Services;
using Stack.Signals;
using Stack.Models;
using Stack.EntitiesBehaviour;

using UnityEngine;

using Zenject;

namespace Stack.Controllers
{
    public class GameController : IInitializable
    {
        protected readonly IPlatformsFactory platformsFactory;
        protected readonly ICutPartsFactory cutPartsFactory;
        protected readonly IPlatfromPlacerService platfromPlacerService;
        protected readonly IPlatformCutterService platformCutterService;
        protected readonly CommonSettingsModel commonSettingsModel;

        protected readonly SignalBus signalBus;

        protected Platform previousPlatform, currentPlatform;

        public GameController(
            IPlatformsFactory platformsFactory,
            ICutPartsFactory cutPartsFactory,
            IPlatfromPlacerService platfromPlacerService,
            CommonSettingsModel commonSettingsModel,
            IPlatformCutterService platformCutterService,
            SignalBus signalBus)
        {
            this.platformsFactory = platformsFactory;
            this.cutPartsFactory = cutPartsFactory;
            this.commonSettingsModel = commonSettingsModel;
            this.platfromPlacerService = platfromPlacerService;
            this.platformCutterService = platformCutterService;
            this.signalBus = signalBus;
        }

        public void Initialize()
        {
            CreateFirstPlatform();
            CreateNewPlatformByCurrent();
        }

        protected virtual void CreateFirstPlatform()
        {
            currentPlatform = platformsFactory
                .CreatePlatform(commonSettingsModel.FirstPlatformPosition, commonSettingsModel.FirstPlatformScale);
        }

        protected virtual void CreateNewPlatformByCurrent()
        {
            Vector3 position, direction;
            float maxDistance;
            platfromPlacerService.GetRandomStartPoint(currentPlatform, out position, out direction, out maxDistance);
            previousPlatform = currentPlatform;
            currentPlatform = platformsFactory.CreatePlatform(position, currentPlatform.transform.localScale);
            currentPlatform.StartMovement(
                direction, 
                position, 
                commonSettingsModel.PlatformStartSpeed,
                maxDistance);

            signalBus.Fire(new PlatformCreatedSignal { CreatedPlatform = currentPlatform });
        }

        public virtual void OnPlatformPlaced()
        {
            var result = platformCutterService.GetNewPlatformAndRemainsPart(previousPlatform, currentPlatform);
            if(result == null)
            {
                cutPartsFactory.CreateCutPart(
                    currentPlatform.transform.position, currentPlatform.transform.localScale);
                MonoBehaviour.Destroy(currentPlatform.gameObject);
                //TODO: game over timer
            }
            else
            {
                currentPlatform.StopMovement();
                if (result.WasRemainsPart)
                {
                    MonoBehaviour.Destroy(currentPlatform.gameObject);
                    currentPlatform = platformsFactory.CreatePlatform(
                        result.NewPlatformPosition, 
                        result.NewPlatformScale);
                    cutPartsFactory.CreateCutPart(
                        result.RemainsPartPosition,
                        result.RemainsPartScale);
                }
                else
                {
                    currentPlatform.transform.position = new Vector3(
                        previousPlatform.transform.position.x,
                        currentPlatform.transform.position.y,
                        previousPlatform.transform.position.z);
                }

                signalBus.Fire(new PlatformStoppedSignal { StoppedPlatform = currentPlatform });
                CreateNewPlatformByCurrent();
            }
        }
    }
}

