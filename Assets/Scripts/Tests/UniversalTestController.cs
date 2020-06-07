using Stack.Models;
using Stack.Services;
using UnityEngine;

namespace Stack.Controllers
{
    public class UniversalTestController : MonoBehaviour
    {
        [SerializeField]
        private Platform baseCube;
        [SerializeField]
        private Platform targetCube;

        private void Start()
        {
            var cubeCutterSevice = new PlatformCutterService();
            bool overlapped;
            var newPlatform = cubeCutterSevice.CreateNewPlatform(baseCube, targetCube, out overlapped);
            if(newPlatform != targetCube)
            {
                Destroy(targetCube.gameObject);
            }

        }
    }
}

