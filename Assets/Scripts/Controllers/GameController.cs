using Stack.Services;
using Stack.Signals;
using Stack.Models;
using Stack.EntitiesBehaviour;

using UnityEngine;

using Zenject;

using System.Collections.Generic;

namespace Stack.Controllers
{
    public class GameController
    {
        protected readonly IPlatformsFactory platformsFactory;
        protected readonly ICutPartsFactory cutPartsFactory;
        protected readonly IPlatfromPlacerService platfromPlacerService;
        protected readonly IPlatformCutterService platformCutterService;
        protected readonly CommonSettingsModel commonSettingsModel;

        protected readonly SignalBus signalBus;

        protected Platform previousPlatform, currentPlatform;
        protected List<GameObject> platforms = new List<GameObject>();

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

        public virtual void OnGameStarted()
        {
            ClearPreviousPlatforms();
            CreateFirstPlatform();
            CreateNewPlatformByCurrent();
        }

        protected virtual void ClearPreviousPlatforms()
        {
            foreach(var platform in platforms)
            {
                if(platform != null)
                {
                    MonoBehaviour.Destroy(platform);
                }
            }
            platforms.Clear();
        }

        protected virtual void CreateFirstPlatform()
        {
            currentPlatform = platformsFactory
                .CreatePlatform(commonSettingsModel.FirstPlatformPosition, commonSettingsModel.FirstPlatformScale);
            platforms.Add(currentPlatform.gameObject);
        }

        protected virtual void CreateNewPlatformByCurrent()
        {
            Vector3 position, direction;
            float maxDistance;
            platfromPlacerService.GetRandomStartPoint(currentPlatform, out position, out direction, out maxDistance);
            previousPlatform = currentPlatform;
            currentPlatform = platformsFactory.CreatePlatform(position, currentPlatform.transform.localScale);
            platforms.Add(currentPlatform.gameObject);
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
                OnPlatformPlacedOutOfPreviousPlatform();
            }
            else
            {
                currentPlatform.StopMovement();

                if (result.WasRemainsPart)
                {
                    OnPlatformPlacedWithOverlapping(result);
                }
                else
                {
                    OnPlatformPlacedExactAbovePreviousPlatform();
                }

                signalBus.Fire(new PlatformStoppedSignal { StoppedPlatform = currentPlatform });
                CreateNewPlatformByCurrent();
            }
        }

        protected virtual void OnPlatformPlacedOutOfPreviousPlatform()
        {
            cutPartsFactory.CreateCutPart(
                    currentPlatform.transform.position, currentPlatform.transform.localScale);
            platforms.Remove(currentPlatform.gameObject);
            MonoBehaviour.Destroy(currentPlatform.gameObject);
            signalBus.Fire<GameOverSignal>();
        }

        protected virtual void OnPlatformPlacedExactAbovePreviousPlatform()
        {
            currentPlatform.transform.position = new Vector3(
                previousPlatform.transform.position.x,
                currentPlatform.transform.position.y,
                previousPlatform.transform.position.z);
        }

        protected virtual void OnPlatformPlacedWithOverlapping(PlatformCutterResultModel result)
        {
            platforms.Remove(currentPlatform.gameObject);
            MonoBehaviour.Destroy(currentPlatform.gameObject);
            currentPlatform = platformsFactory.CreatePlatform(
                result.NewPlatformPosition,
                result.NewPlatformScale);
            cutPartsFactory.CreateCutPart(
                result.RemainsPartPosition,
                result.RemainsPartScale);
            platforms.Add(currentPlatform.gameObject);
        }
    }
}

