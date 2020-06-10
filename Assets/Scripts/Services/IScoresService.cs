namespace Stack.Services
{
    public interface IScoresService
    {
        int GetCurrentScore();
        void IncreaseCurrentScore(int increment);
        void ResetCurrentScore();
        void SaveCurrentScoreIfHigher();
        int? GetHighestScore();
    }
}

