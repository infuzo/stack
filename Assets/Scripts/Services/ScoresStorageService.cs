using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Services
{
    class ScoresStorageService : IScoresStorageService
    {
        public virtual int? GetHighestScore()
        {
            throw new NotImplementedException();
        }

        public virtual void SetHighestScore(int score)
        {
            throw new NotImplementedException();
        }
    }
}
