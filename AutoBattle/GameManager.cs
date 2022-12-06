using System;
using System.Collections.Generic;
using System.Linq;
using AutoBattle.CharacterActions;
using AutoBattle.CharacterClasses;

namespace AutoBattle
{
    public class GameManager
    { 
        Battlefield _battlefield;
        List<Character> _characters = new List<Character>();

        public void StartGame()
        {
            SetupGame();
            StartGameLoop();
        }

        public void SetupGame()
        {
            Console.WriteLine("Welcome To AutoBattler " + Environment.NewLine + Environment.NewLine);

            InitializationInputData inputData = new InitializationInputReader().ReadData();
            _battlefield = new Battlefield(inputData.BattlefieldSize);
            _characters = CreateCharacters(inputData.PlayerCharactersClasses, inputData.PlayerTeam);

            _battlefield.PlaceEntitiesRandomly(_characters.ConvertAll(character => character as IBattlefieldEntity));
        }

        List<Character> CreateCharacters(IReadOnlyList<ACharacterClass> playerCharactersClasses, TeamData playerTeam)
        {
            List<Character> characters = new List<Character>();
            List<TeamData> remainingTeams = GameConstants.TEAMS.ToList().FindAll(team => team != playerTeam);
            int teamSize = playerCharactersClasses.Count;

            characters.AddRange(CreateCharactersFromTeam(playerCharactersClasses, playerTeam));
           
            foreach(TeamData team in remainingTeams)
            {
                IReadOnlyList<ACharacterClass> randomClasses = GetRandomCharacterClasses(teamSize);
                characters.AddRange(CreateCharactersFromTeam(randomClasses, team));
            }

            return characters;
        }

        IReadOnlyList<Character> CreateCharactersFromTeam(IReadOnlyList<ACharacterClass> charactersClasses, TeamData team)
        {
            List<Character> charactersCreated = new List<Character>();

            for(int i = 0; i < charactersClasses.Count; i++)
            {
                charactersCreated.Add(new Character(charactersClasses[i], team, i));
            }

            return charactersCreated;
        }

        private IReadOnlyList<ACharacterClass> GetRandomCharacterClasses(int amount)
        {
            List<ACharacterClass> randomClasses = new List<ACharacterClass>();
            for (int i = 0; i < amount; i++)
            {
                randomClasses.Add(GameConstants.ALL_CLASSES.GetRandomElement());
            }
            return randomClasses;
        }


        public void StartGameLoop()
        {
            _battlefield.Draw();
            Console.Write(Environment.NewLine);
            Console.WriteLine("Click on any key to start the game...\n");
            Console.Write(Environment.NewLine);
            Console.ReadKey();

            bool matchIsOver = false;

            while (!matchIsOver)
            {
                foreach (Character character in _characters)
                {
                    if (character.IsDead)
                        continue;

                    HandleTurn(character);

                    if (CheckIfMatchEnded())
                    {
                        matchIsOver = true;
                        break;
                    }

                    Console.WriteLine($"{Environment.NewLine}Click on any key to start the next turn..." +
                                $"{Environment.NewLine}{Environment.NewLine}");

                    Console.ReadKey();
                }
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine(GetMatchResult());

        }

        private bool CheckIfMatchEnded()
        {
            List<Character> aliveCharacters = _characters.FindAll(character => !character.IsDead);

            return aliveCharacters.Count == 0 ||
                aliveCharacters.TrueForAll(character => character.Team == aliveCharacters[0].Team);
        }

        private string GetMatchResult()
        {
            Character winner = _characters.Find(character => !character.IsDead);

            if (winner == null)
                return "The Match Was a Tie! No Team Won";

            return $"The winner was Team {winner.Team}";
        }


        public void HandleTurn(Character character)
        {
            Character target = GetNearestTarget(character);

            IEnumerable<ICharacterAction> turnActions = character.DecideTurnActions(target);
            string formatedMessage = string.Empty;
            foreach (ICharacterAction action in turnActions)
            {
                string outputMessage = action.Execute(character, target, _battlefield);
                formatedMessage += $"-> {outputMessage}{Environment.NewLine}";
            }
            
            if (target.IsDead)
            {
                formatedMessage += $"-> {target.DisplaySymbol} has died !{Environment.NewLine}";
            }

            _battlefield.Draw();
            Console.WriteLine(formatedMessage + Environment.NewLine);
        }

        private Character GetNearestTarget(Character character)
        {
            IEnumerable<Character> validTargets = GetValidTargets(character);

            Character nearestTarget = null;
            int nearestDistance = int.MaxValue;

            foreach (Character target in validTargets)
            {
                int targetDistance = character.Position.Distance(target.Position);

                if (nearestDistance > targetDistance)
                {
                    nearestTarget = target;
                    nearestDistance = targetDistance;
                }
            }

            return nearestTarget;
        }

        private IReadOnlyList<Character> GetValidTargets(Character character)
        {
            return _characters.FindAll(target =>
                    target != character &
                    !target.IsDead &
                    target.Team != character.Team);
        }
    }
}
