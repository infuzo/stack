using UnityEngine;
using Stack.EntitiesBehaviour;
using Stack.Models;

namespace Stack.Services
{
    public class PlatformPlacerService : IPlatfromPlacerService
    {
        protected readonly StartPointsModel startPointsModel;

        public PlatformPlacerService(StartPointsModel startPointsModel)
        {
            this.startPointsModel = startPointsModel;
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
    }
}

