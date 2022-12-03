using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Game
    {
        Grid grid;
        
        Character _playerCharacter;
        Character _enemyCharacter;
        List<Character> _characters = new List<Character>();
        bool _matchIsOver = false;


        CharacterClass playerCharacterClass;
        GridBox _playerCurrentLocation;
        GridBox _enemyCurrentLocation;
        //int _numberOfPossibleTiles = grid.grids.Count;
        int _currentTurn = 0;

        public void StartGame()
        {
            InitializeGameBoard();
            //StartGameLoop();
        }


        public void InitializeGameBoard()
        {
            CharacterClass playerClass = GetPlayerClassInput();
            CharacterClass enemyClass = ChooseEnemyClass();
            GridSize gridSize = GetGridSizeInput();

            grid = new Grid(gridSize);

            //TODO da pra deixar isso mais generico mas se segura pq nao vale a pena pq isso vai mudar com o time
            InitializeCharacters(playerClass, enemyClass);

            AlocatePlayers();
        }

        private void InitializeCharacters(CharacterClass playerClass, CharacterClass enemyClass)
        {
            //TODO Tirar o PlayerIndex
            _playerCharacter = new Character(playerClass)
            {
                PlayerIndex = 0
            };
            _enemyCharacter = new Character(enemyClass)
            {
                PlayerIndex = 1
            };

            //TODO Fazer o target Dinamico;
            _enemyCharacter.Target = _playerCharacter;
            _playerCharacter.Target = _enemyCharacter;

            _characters = new List<Character> {
                _playerCharacter,
                _enemyCharacter
            };
        }

        CharacterClass GetPlayerClassInput()
        {
            int input = 0;

            while (input < 1 || input > 4)
            {
                Console.WriteLine("Choose Between One of this Classes:\n");
                foreach(CharacterClass character in Enum.GetValues(typeof(CharacterClass)))
                {
                    Console.Write($"[{(int)character}] {character} ");
                }
                Console.Write("\n");
                
                TryReadInt(out input);

                //TODO: printar um erro se o input tiver errado 
            }
             
            CharacterClass chosenClass = (CharacterClass) input;

            Console.WriteLine($"Player Class Choice: {chosenClass}");
            return chosenClass;
        }

        GridSize GetGridSizeInput()
        {
            GridSize gridSize = default;


            //TODO : Mudar o jeito de pegar esse input para algo mais intuitivo
            while (!gridSize.IsValid())
            {
                Console.WriteLine("Choose the width of the battlefield.\n");
                TryReadInt(out gridSize.Width);

                Console.WriteLine("Choose the height of the battlefield.\n");
                TryReadInt(out gridSize.Height);
                //TODO: printar um erro se o input tiver errado 
            }


            Console.WriteLine("The battle field has been created\n");

            return gridSize;
        }

        private CharacterClass ChooseEnemyClass()
        {
            int numberOfClasses = Enum.GetValues(typeof(CharacterClass)).Length;
            int randomIndex = GetRandomInt(1, numberOfClasses);
            CharacterClass enemyClass = (CharacterClass)randomIndex;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");

            return enemyClass;
        }

        public void StartGameLoop()
        {
            while (!_matchIsOver)
            {
                //TODO: pq eu faria esse sort? nao sei
                //if (currentTurn == 0)
                //{
                //    //AllPlayers.Sort();  
                //}

                foreach (Character character in _characters)
                {
                    character.StartTurn(grid);
                }

                _currentTurn++;

                if (_playerCharacter.Health == 0 || _enemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    _matchIsOver = true;
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                }
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                }
            }
        }


        //--------------------------------------------------------------------------------------------------------------

        
        void AlocatePlayers()
        {
            AlocatePlayerCharacter();

        }

        void AlocatePlayerCharacter()
        {
            int random = 0;
            GridBox RandomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                GridBox PlayerCurrentLocation = RandomLocation;
                RandomLocation.ocupied = true;
                grid.grids[random] = RandomLocation;
                _playerCharacter.currentBox = grid.grids[random];
                AlocateEnemyCharacter();
            }
            else
            {
                AlocatePlayerCharacter();
            }
        }

        void AlocateEnemyCharacter()
        {
            int random = 24;
            GridBox RandomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                _enemyCurrentLocation = RandomLocation;
                RandomLocation.ocupied = true;
                grid.grids[random] = RandomLocation;
                _enemyCharacter.currentBox = grid.grids[random];
                grid.drawBattlefield(5, 5);
            }
            else
            {
                AlocateEnemyCharacter();
            }
        }

        int GetRandomInt(int min, int max)
        {
            var rand = new Random();
            int index = rand.Next(min, max);
            //TODO: Checar se iss eh max inclusivo
            return index;
        }

        //TODO: Extrair para um utils?
        bool TryReadInt(out int value)
        {
            string input = Console.ReadLine();
            return Int32.TryParse(input, out value);
        }
    }
}

// -> Coloquei os métodos que estao encadeados um no outro no fluxo do método principal,
// para tornar o fluxo do turno mais legivel
//
// -> Separei o método start Game em um método de inicialização do board e um método para começar o game loop
//
// -> Retirei a recursão presente entre o start turn e o handle turn e
// transformei num while que vai representar o game loop
// (onde vai ser controlado os turnos)
//
//
// -> Deletei o Start Turn e o Handle Turn e
// coloquei suas implementaççoes no while do game loop pra modularizar melhor depois
//
//
// -> Tirei a recursão do método de pegar o input da classe do personagem, dessa forma, não deixando funções penduradas
//      na memória a cada erro. Também tirei o switch Case da função, pois ele era desnecessário se fosse feito o parsing
//      do input para int direto. Por fim separei a responsabilidade dessa função para so pegar o input e retorna-lo,
//      extraindo a funcianalidade de criar o personagem.
//
// -> Criei método para ler o gridSize e criei struct para guardar a informação do GridSize
//
//
// -> Troquei todos os parses de string para int para tryParse para tratar os casos que não escreviam números
// -> Criei método  para ler um int do console;
//
// -> Extrair o funcionalidade de escolher a classe do inimigo e deletei os métodos de criação do player e dos inimigos,
//      substituindo pelo construtor de Character. Coloquei a inicialização dos status no Construtor do Character
// ;

