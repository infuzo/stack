namespace Stack.Services
{
    public class ScoresService : IScoresService
    {
        protected readonly IScoresStorageService scoresStorageService;

        protected int currentScore = 0;

        public ScoresService(IScoresStorageService scoresStorageService)
        {
            this.scoresStorageService = scoresStorageService;
        }

        public virtual int GetCurrentScore()
        {
            return currentScore;
        }

        public virtual void IncreaseCurrentScore(int increment = 1)
        {
            currentScore += increment;
        }

        public virtual void ResetCurrentScore()
        {
            currentScore = 0;
        }

        public virtual void SaveCurrentScoreIfHigher()
        {
            var highestScore = scoresStorageService.GetHighestScore();
            if (highestScore != null && currentScore > highestScore)
            {
                scoresStorageService.SetHighestScore(currentScore);
            }
        }

        public virtual int? GetHighestScore()
        {
            return scoresStorageService.GetHighestScore();
        }
    }
}

