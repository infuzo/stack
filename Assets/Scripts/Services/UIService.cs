using UnityEngine;
using UnityEngine.UI;

namespace Stack.Services
{
    public class UIService : MonoBehaviour, IUIService
    {
        [SerializeField]
        private Text scoresCaption;

        public void UpdateScores(int scores)
        {
            scoresCaption.text = scores.ToString();
        }
    }
}

