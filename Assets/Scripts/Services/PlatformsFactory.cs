using UnityEngine;

using Stack.Models;
using Stack.EntitiesBehaviour;

namespace Stack.Services
{
    public class PlatformsFactory : IPlatformsFactory
    {
        private readonly CommonPrefabsModel commonPrefabsModel;

        public PlatformsFactory(
            CommonPrefabsModel commonPrefabsModel)
        {
            this.commonPrefabsModel = commonPrefabsModel;
        }

        public virtual Platform CreatePlatform(
            Vector3 position,
            Vector3 scale)
        {
            var newObject = MonoBehaviour.Instantiate<Platform>(commonPrefabsModel.PlatformPrefab);
            newObject.transform.position = position;
            newObject.transform.localScale = scale;
            return newObject;
        }
    }
}

