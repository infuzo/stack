using UnityEngine;

using Stack.Models;
using Stack.EntitiesBehaviour;

namespace Stack.Services
{
    public class CutPartsFactory : ICutPartsFactory
    {
        protected readonly CommonSettingsModel commonSettingsModel;
        protected readonly CommonPrefabsModel commonPrefabsModel;

        public CutPartsFactory(CommonSettingsModel commonSettingsModel,
            CommonPrefabsModel commonPrefabsModel)
        {
            this.commonSettingsModel = commonSettingsModel;
            this.commonPrefabsModel = commonPrefabsModel;
        }

        public virtual CutPart CreateCutPart(
            Vector3 position, 
            Vector3 scale)
        {
            var cutPart = MonoBehaviour.Instantiate(commonPrefabsModel.CutPartPrefab);
            cutPart.transform.position = position;
            cutPart.transform.localScale = scale;
            cutPart.Configure(commonSettingsModel.TimeInSecondsToDestroyCutPart);
            return cutPart;
        }
    }
}

