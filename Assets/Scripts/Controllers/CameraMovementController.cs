using UnityEngine;

using Stack.EntitiesBehaviour;
using Stack.Models;

using Zenject;
using System.Collections;

namespace Stack.Controllers
{
    public class CameraMovementController : MonoBehaviour
    {
        [SerializeField]
        private new Camera camera;

        [Inject]
        private CommonSettingsModel commonSettingsModel;

        private float startCameraY;
        private Coroutine coroutineCameraChangePosition;

        private void Awake()
        {
            startCameraY = camera.transform.position.y;
        }

        public void OnNewPlatformCreated(Platform platform)
        {
            if(coroutineCameraChangePosition != null)
            {
                StopCoroutine(coroutineCameraChangePosition);
            }
            coroutineCameraChangePosition = StartCoroutine(
                CoroutineCameraChangePosition(startCameraY + platform.transform.position.y));
        }

        IEnumerator CoroutineCameraChangePosition(float newYPosition)
        {
            var cameraStartPosition = camera.transform.position;
            var cameraEndPosition = new Vector3(
                cameraStartPosition.x,
                newYPosition,
                cameraStartPosition.z);

            float factor = 0f;
            while(factor < 1f)
            {
                factor += Time.deltaTime * commonSettingsModel.CameraMovementSpeed;
                camera.transform.position = Vector3.Lerp(cameraStartPosition, cameraEndPosition, factor);
                yield return new WaitForEndOfFrame();
            }

            camera.transform.position = cameraEndPosition;
            coroutineCameraChangePosition = null;
        }
    }
}

