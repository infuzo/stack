using System.Collections;

using UnityEngine;

using Zenject;

using Stack.Models;

namespace Stack.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [Inject]
        private CommonSettingsModel commonSettingsModel;

        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private GameObject mainMenuGameObject;

        private Coroutine coroutineShowingAnimation;

        public void OnGameOver()
        {
            StartCoroutineShowingAnimation();
        }

        private void StartCoroutineShowingAnimation()
        {
            if(coroutineShowingAnimation != null)
            {
                StopCoroutine(coroutineShowingAnimation);
            }
            mainMenuGameObject.SetActive(true);
            coroutineShowingAnimation = StartCoroutine(CoroutineShowingAnimation(
                commonSettingsModel.MainMenuDelayAfterGameOver, 2f));
        }

        private IEnumerator CoroutineShowingAnimation(float delay, float speed)
        {
            canvasGroup.alpha = 0f;
            yield return new WaitForSeconds(delay);

            float counter = 0f;
            while(counter < 1f)
            {
                counter += Time.unscaledDeltaTime * speed;
                canvasGroup.alpha = Mathf.Clamp01(counter);
                yield return new WaitForEndOfFrame();
            }

            coroutineShowingAnimation = null;
        }
    }
}

