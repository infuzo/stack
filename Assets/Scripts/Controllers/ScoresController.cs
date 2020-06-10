using Stack.Services;

using Zenject;

namespace Stack.Controllers
{
    public class ScoresController : IInitializable
    {
        private IScoresService scoresService;

        public ScoresController(IScoresService scoresService)
        {
            this.scoresService = scoresService;
        }

        public void Initialize()
        {
            scoresService.ResetCurrentScore();
        }

        public void OnPlatformStopped()
        {
            scoresService.IncreaseCurrentScore(1);
            UnityEngine.Debug.Log(scoresService.GetCurrentScore());
        }        
    }
}

