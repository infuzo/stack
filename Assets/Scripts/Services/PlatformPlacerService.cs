using UnityEngine;
using Stack.EntitiesBehaviour;
using Stack.Models;

namespace Stack.Services
{
    public class PlatformPlacerService : IPlatfromPlacerService
    {
        protected readonly StartPointsModel startPointsModel;
        protected readonly Vector3 defaultLeftTopPosition, defaultLeftBottomPosition, defaultRightTopPosition, defaultRightBottomPosition;

        public PlatformPlacerService(StartPointsModel startPointsModel)
        {
            this.startPointsModel = startPointsModel;

            defaultLeftTopPosition = startPointsModel.LeftTopPoint.position;
            defaultLeftBottomPosition = startPointsModel.LeftBottomPoint.position;
            defaultRightTopPosition = startPointsModel.RightTopPoint.position;
            defaultRightBottomPosition = startPointsModel.RightBottomPoint.position;
        }

        public virtual void GetRandomStartPoint(
            Platform basePlatform,
            out Vector3 position,
            out Vector3 direction,
            out float maxDistance)
        {
            //scale of start point is movement direction
            var point = startPointsModel.GetRandomPoint();

            position = new Vector3(point.position.x,
                basePlatform.transform.position.y + basePlatform.transform.localScale.y,
                point.position.z);
            direction = point.localScale;
            maxDistance = new Vector2(position.x, position.z).magnitude * 2f;
        }

        public virtual void OnGameOver()
        {
            startPointsModel.LeftTopPoint.position = defaultLeftTopPosition;
            startPointsModel.LeftBottomPoint.position = defaultLeftBottomPosition;
            startPointsModel.RightTopPoint.position = defaultRightTopPosition;
            startPointsModel.RightBottomPoint.position = defaultRightBottomPosition;
        }

        public virtual void OnNewCurrentPlatformCreated(Platform platform)
        {
            startPointsModel.RightTopPoint.position = new Vector3(
                startPointsModel.RightTopPoint.position.x,
                startPointsModel.RightTopPoint.position.y,
                platform.transform.position.z);
            startPointsModel.RightBottomPoint.position = new Vector3(
                platform.transform.position.x,
                startPointsModel.RightBottomPoint.position.y,
                startPointsModel.RightBottomPoint.position.z);
            startPointsModel.LeftTopPoint.position = new Vector3(
                platform.transform.position.x,
                startPointsModel.LeftTopPoint.position.y,
                startPointsModel.LeftTopPoint.position.z);
            startPointsModel.LeftBottomPoint.position = new Vector3(
                startPointsModel.LeftBottomPoint.position.x,
                startPointsModel.LeftBottomPoint.position.y,
                platform.transform.position.z);
        }
    }
}

