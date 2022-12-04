using System;

namespace AutoBattle
{
    public static class Utils
    {
        static Random _randomGenerator = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            //TODO: Checar se iss eh max inclusivo
            return _randomGenerator.Next(min, max);
        }

        public static bool TryReadInt(out int value)
        {
            string input = Console.ReadLine();
            return Int32.TryParse(input, out value);
        }
    }
}




// -> Criei uma classe de Utils para deixar centralizado Comportamentos
//    que genericos que seriam utilizado pleo projeto todo

