﻿using Zenject;
using Stack.Models;
using Stack.Services;
using Stack.Controllers;
using UnityEngine;
using Stack.Signals;
using Stack.EntitiesBehaviour;

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
            InstallSignals();
        }

        private void InstallServices()
        {
            Container
                .Bind<IPlatformsFactory>()
                .To<PlatformsFactory>()
                .AsSingle();
            Container
                .Bind<ICutPartsFactory>()
                .To<CutPartsFactory>()
                .AsSingle();
            Container
                .Bind<IPlatfromPlacerService>()
                .To<PlatformPlacerService>()
                .AsSingle()
                .WithArguments<StartPointsModel>(startPointsModel);
            Container
                .Bind<IPlatformCutterService>()
                .To<PlatformCutterService>()
                .AsSingle()
                .WithArguments(commonSettingsModel.MaxOffsetToSetAsNotOverlapped);
            Container
                .Bind<IScoresStorageService>()
                .To<ScoresStorageService>()
                .AsSingle();
            Container
                .Bind<IScoresService>()
                .To<ScoresService>()
                .AsSingle();
            Container
                .Bind<IUIService>()
                .To<UIService>()
                .FromComponentInHierarchy()
                .AsSingle();
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
            Container
                .Bind<CameraMovementController>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ScoresController>()
                .AsSingle();
            Container
                .Bind<MainMenuController>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void InstallSignals() //todo: separate signal installer
        {
            GameStartedSignal();
            InstallPlatformPlacedSignal();
            InstallPlatformStoppedSignal();
            InstallPlatformCreatedSignal();
            InstallGameOverSignal();
        }

        private void GameStartedSignal()
        {
            Container.DeclareSignal<GameStartedSignal>();
            Container
                .BindSignal<GameStartedSignal>()
                .ToMethod<GameController>(h => h.OnGameStarted)
                .FromResolve();
            Container
                .BindSignal<GameStartedSignal>()
                .ToMethod<IUIService>((h, s) => h.UpdateScores(0))
                .FromResolve();
        }

        private void InstallPlatformPlacedSignal()
        {
            Container.DeclareSignal<PlatformPlacedSignal>();
            Container
                .BindSignal<PlatformPlacedSignal>()
                .ToMethod<GameController>(h => h.OnPlatformPlaced)
                .FromResolve();
        }

        private void InstallPlatformStoppedSignal()
        {
            Container.DeclareSignal<PlatformStoppedSignal>();
            Container
                .BindSignal<PlatformStoppedSignal>()
                .ToMethod<IPlatfromPlacerService>(
                    (handler, signal) => (handler as PlatformPlacerService).OnNewCurrentPlatformCreated(signal.StoppedPlatform))
                .FromResolve();
            Container
                .BindSignal<PlatformStoppedSignal>()
                .ToMethod<ScoresController>(handler => handler.OnPlatformStopped)
                .FromResolve();
        }

        private void InstallPlatformCreatedSignal()
        {
            Container.DeclareSignal<PlatformCreatedSignal>();
            Container
                .BindSignal<PlatformCreatedSignal>()
                .ToMethod<CameraMovementController>(
                    (handler, signal) => handler.OnNewPlatformCreated(signal.CreatedPlatform))
                .FromResolve();
        }

        private void InstallGameOverSignal()
        {
            Container.DeclareSignal<GameOverSignal>();

            Container
                .BindSignal<GameOverSignal>()
                .ToMethod<CameraMovementController>(handler => handler.OnGameOver)
                .FromResolve(); 
            Container
                 .BindSignal<GameOverSignal>()
                 .ToMethod<ScoresController>(handler => handler.OnGameOver)
                 .FromResolve();
            Container
                 .BindSignal<GameOverSignal>()
                 .ToMethod<MainMenuController>(handler => handler.OnGameOver)
                 .FromResolve();
            Container
                 .BindSignal<GameOverSignal>()
                 .ToMethod<IPlatfromPlacerService>(handler => (handler as PlatformPlacerService).OnGameOver)
                 .FromResolve();
        }
    }
}

