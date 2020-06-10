using System;

using UnityEngine;

namespace Stack.Services
{
    public class ScoresStorageService : IScoresStorageService
    {
        private const string highestScoreKey = "highestScore";

        public virtual int? GetHighestScore()
        {
            try
            {
                if (PlayerPrefs.HasKey(highestScoreKey))
                {
                    return PlayerPrefs.GetInt(highestScoreKey);
                }
            }
            catch(PlayerPrefsException exception)
            {
                Debug.LogException(exception);
            }

            return null;
        }

        public virtual void SetHighestScore(int score)
        {
            try
            {
                PlayerPrefs.SetInt(highestScoreKey, score);
                PlayerPrefs.Save();
            }
            catch (PlayerPrefsException exception)
            {
                Debug.LogException(exception);
            }
        }
    }
}
