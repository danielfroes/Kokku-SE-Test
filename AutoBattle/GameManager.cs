using System;
using System.Collections.Generic;
using System.Linq;
using AutoBattle.CharacterActions;
using AutoBattle.CharacterClasses;

namespace AutoBattle
{
    class GameManager 
    {
        Battlefield _battlefield;

        List<Character> _characters = new List<Character>();
        bool _matchIsOver = false;

        readonly IReadOnlyList<ICharacterClass> _characterClasses = new List<ICharacterClass>
        {
            new PaladinClass(),
            new ArcherClass(),
        };

        public void StartGame()
        {
            InitializeGameBoard();
            StartGameLoop();
        }

        public void InitializeGameBoard()
        {
            //ICharacterClass playerClass = GetPlayerClassInput();
            ICharacterClass playerClass = ChooseEnemyClass();
            ICharacterClass enemyClass = ChooseEnemyClass();

            //Size battlefieldSize = GetBattlefieldSizeInput();

            Size battlefieldSize = new Size()
            {
                Height = 10,
                Width = 10,
            };

            //TODO da pra deixar isso mais generico mas se segura pq nao vale a pena pq isso vai mudar com o time
            _characters = InitializeCharacters(playerClass, enemyClass);

            _battlefield = new Battlefield(battlefieldSize);
            _battlefield.PlaceEntitiesRandomly(_characters.Cast<IBattlefieldEntity>());
        }


        private List<Character> InitializeCharacters(ICharacterClass playerClass, ICharacterClass enemyClass)
        {
            return new List<Character> {
                new Character(ChooseEnemyClass(), "a"),
                new Character(ChooseEnemyClass(), "b"),
                new Character(ChooseEnemyClass(), "c"),
                new Character(ChooseEnemyClass(), "d"),
                new Character(ChooseEnemyClass(), "e"),
                new Character(ChooseEnemyClass(), "f"),
                new Character(ChooseEnemyClass(), "g"),
            };
        }

        #region Input Handling
        ICharacterClass GetPlayerClassInput()
        {
            int input = -1;

            while (input < 0 || input >= _characterClasses.Count)
            {
                Console.WriteLine("Choose Between One of this Classes:\n");
                
                for(int i = 0; i < _characterClasses.Count; i++)
                {
                    Console.Write($"[{i}] {_characterClasses[i].DisplayName} ");
                }

                Console.Write("\n");
                
                Utils.TryReadInt(out input);

                //TODO: printar um erro se o input tiver errado 
            }
             
            ICharacterClass chosenClass = _characterClasses[input];

            Console.WriteLine($"Player Class Choice: {chosenClass.DisplayName}");
            return chosenClass;
        }

        Size GetBattlefieldSizeInput()
        {
            Size battlefieldSize = default;


            //TODO : Mudar o jeito de pegar esse input para algo mais intuitivo
            while (!battlefieldSize.IsValid())
            {
                Console.WriteLine("Choose the width of the battlefield.\n");
                Utils.TryReadInt(out battlefieldSize.Width);

                Console.WriteLine("Choose the height of the battlefield.\n");
                Utils.TryReadInt(out battlefieldSize.Height);

                //TODO: checar aqui se o numero de celulas cabem o numero de players
                //TODO: printar um erro se o input tiver errado 
            }


            Console.WriteLine("The battle field has been created\n");

            return battlefieldSize;
        }

        private ICharacterClass ChooseEnemyClass()
        {
            ICharacterClass enemyClass = _characterClasses.GetRandomElement();
            // Console.WriteLine($"Enemy Class Choice: {enemyClass.DisplayName}");
             
            return enemyClass;
        }

        #endregion

        public void StartGameLoop()
        {
            _battlefield.Draw();
            Console.Write(Environment.NewLine);
            Console.WriteLine("Click on any key to start the game...\n");
            Console.Write(Environment.NewLine);
            Console.ReadKey();

            while (!_matchIsOver)
            {
                foreach (Character character in _characters)
                {
                    if (character.IsDead)
                        continue;

                    HandleTurn(character);
                    
                    if (CheckIfMatchEnded())
                    {
                        _matchIsOver = true;
                        break;
                    }

                    Console.WriteLine(Environment.NewLine + "Click on any key to start the next turn..."
                        + Environment.NewLine + Environment.NewLine);
                    Console.ReadKey();
                }
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine(GetMatchResult());

        }

        private string GetMatchResult()
        {
            Character winner = _characters.Find(character => !character.IsDead);
            if (winner == null)
                return "The Match Was a Tie!";

            return $"The winner was {winner.DisplaySymbol}";
        }

        private bool CheckIfMatchEnded()
        {
            List<Character> deadCharacters = _characters.FindAll(character => character.IsDead);
            return deadCharacters.Count >= _characters.Count - 1;
        }

        public void HandleTurn(Character character)
        {
            Character target = GetNearestTarget(character);
            // TODO : lidar com o caso que retorna null?

            ICharacterAction turnAction = character.DecideAction(target);
            string outputMessage = turnAction.Execute(character, target, _battlefield);
            Console.WriteLine(outputMessage);

            if (target.IsDead)
            {
                Console.WriteLine($"{target.DisplaySymbol} has died !\n");
            }

            _battlefield.Draw();
        }

        private Character GetNearestTarget(Character character)
        {
            List<Character> possibleTargets = 
                _characters.FindAll(target => target != character && !target.IsDead);

            Character nearestTarget = null;
            int nearestDistance = int.MaxValue;

            foreach (Character possibleTarget in possibleTargets)
            {
                int targetDistance = character.Position.Distance(possibleTarget.Position);

                if (nearestDistance > targetDistance)
                {
                    nearestTarget = possibleTarget;
                    nearestDistance = targetDistance;
                }
            }

            return nearestTarget;
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
//      substituindo pelo construtor de Character. Coloquei a inicialização dos status no Construtor do Character]
//
// -> Coloquei a responsabilidade de achar uma grid vazia para um método dentro da grid e tirei a recursão do AllocatePlayers
// -> Deletei as duas funções de AlocatePlayer e AlocateEnemys e fiz um foreach chamando a placeEntityInBattlefield
//
// -> Refiz todo o fluxo do turno para deixar as responsabilidades mais modularizadas e código mais escalavel e legivel
// -> O dano deixou de ser Random e agr está seguindo uma formula.
// 
// -> Fiz um método para achar o target dinamicamente a cada turno;
// ;

