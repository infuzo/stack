using Stack.Services;

using Zenject;

namespace Stack.Controllers
{
    public class ScoresController : IInitializable
    {
        private IScoresService scoresService;
        private IUIService uiService;

        public ScoresController(
            IScoresService scoresService,
            IUIService uiService)
        {
            this.scoresService = scoresService;
            this.uiService = uiService;
        }

        public void Initialize()
        {
            scoresService.ResetCurrentScore();
            uiService.UpdateScores(scoresService.GetCurrentScore());
        }

        public void OnPlatformStopped()
        {
            scoresService.IncreaseCurrentScore(1);
            uiService.UpdateScores(scoresService.GetCurrentScore());
        }

        public void OnGameOver()
        {
            scoresService.SaveCurrentScoreIfHigher();
        }
    }
}

