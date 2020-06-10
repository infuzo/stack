namespace Stack.Services
{
    public interface IScoresStorageService
    {
        int? GetHighestScore();
        void SetHighestScore(int score);
    }
}
