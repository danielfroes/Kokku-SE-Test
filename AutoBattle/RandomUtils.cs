using System;

namespace AutoBattle
{
    public static class RandomUtils
    {
        static Random _randomGenerator = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            //TODO: Checar se iss eh max inclusivo
            return _randomGenerator.Next(min, max);
        }

        
    }
}


