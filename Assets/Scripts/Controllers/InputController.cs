using Stack.Signals;

using UnityEngine.EventSystems;
using UnityEngine;

using Zenject;

namespace Stack.Controllers
{
    public class InputController : MonoBehaviour, IPointerDownHandler
    {
        [Inject]
        protected SignalBus signalBus;

        public void OnPointerDown(PointerEventData eventData)
        {
            signalBus.Fire<PlatformPlacedSignal>();
        }
    }

}
