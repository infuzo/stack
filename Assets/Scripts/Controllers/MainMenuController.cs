using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

using Stack.Models;
using Stack.Signals;
using Stack.Services;

namespace Stack.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [Inject]
        private CommonSettingsModel commonSettingsModel;
        [Inject]
        private SignalBus signalBus;
        [Inject]
        private IScoresService scoresService;

        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private GameObject mainMenuGameObject;
        [SerializeField]
        private GameObject bestScoreParent;
        [SerializeField]
        private Text textBestScore;

        private Coroutine coroutineShowingAnimation;
        private bool tapStartGameAvailable;

        private void Start()
        {
            UpdateBestScore();
            mainMenuGameObject.SetActive(true);
            tapStartGameAvailable = true;
            canvasGroup.alpha = 1f;
        }

        public void OnTap()
        {
            if(!tapStartGameAvailable) { return; }

            signalBus.Fire<GameStartedSignal>();
            tapStartGameAvailable = false;
            mainMenuGameObject.SetActive(false);
        }

        public void OnGameOver()
        {
            StartCoroutineShowingAnimation();
            UpdateBestScore();
        }

        private void UpdateBestScore()
        {
            var bestScore = scoresService.GetHighestScore();
            bestScoreParent.SetActive(bestScore != null);
            if(bestScore != null)
            {
                textBestScore.text = bestScore.ToString();
            }
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

            canvasGroup.alpha = 1f;
            tapStartGameAvailable = true;

            coroutineShowingAnimation = null;
        }
    }
}

