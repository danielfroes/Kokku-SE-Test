using System.ComponentModel.DataAnnotations;

namespace AutoBattle
{
    class Program
    {
        //TODO: Ver se esse tipo de comportamento eh normal com .NET
        static void Main(string[] args)
        {
            GameManager gameInstance = new GameManager();

            gameInstance.StartGame();

        }
    }
}

// -> Criei a classe Game, talvez vire GameManager, para encapsular o estado do jogo inves de estar responsavel para dentro da main




