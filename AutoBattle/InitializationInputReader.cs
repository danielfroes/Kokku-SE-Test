using System;
using System.Collections.Generic;
using AutoBattle.CharacterClasses;

namespace AutoBattle
{
    public struct InitializationInputData
    {
        public Size BattlefieldSize { get; }
        public TeamData PlayerTeam { get; }
        public IReadOnlyList<ACharacterClass> PlayerCharactersClasses { get; }

        public InitializationInputData(Size battlefieldSize,
            TeamData playerTeam, 
            IReadOnlyList<ACharacterClass> playerTeamClasses)
        {
            BattlefieldSize = battlefieldSize;
            PlayerTeam = playerTeam;
            PlayerCharactersClasses = playerTeamClasses;
        }
    }

    public class InitializationInputReader
    {    
        public InitializationInputData ReadData()
        {
            Size battlefieldSize = GetBattlefieldSizeInput();
            int teamSize = GetPlayerTeamSizeInput(battlefieldSize);
            TeamData playerTeam = GetPlayerTeamInput();
            IReadOnlyList<ACharacterClass> playerCharacterClasses = GetPlayerTeamClassesInput(teamSize);

            return new InitializationInputData(battlefieldSize, playerTeam, playerCharacterClasses);
        }

        private TeamData GetPlayerTeamInput()
        {
            int input = -1;
            bool inputIsValid = false;

            IReadOnlyList<TeamData> availableTeams = GameConstants.TEAMS;

            while (!inputIsValid)
            {
                Console.WriteLine("Choose Between One of this Teams:" + Environment.NewLine);
                for (int i = 0; i < availableTeams.Count; i++)
                {
                    Console.Write($"[{i}] {availableTeams[i]} ");
                }
                Console.Write(Environment.NewLine);

                inputIsValid = TryReadInt(out input);
                inputIsValid &= input >= 0 & input < availableTeams.Count;

                if (!inputIsValid)
                {
                    Console.WriteLine($"ERROR: The class selected does not exist. Try Again.");
                }
            }

            TeamData chosenTeam = availableTeams[input];
            Console.WriteLine($"Player Team Choice: {chosenTeam}");
            return chosenTeam;
        }

        IReadOnlyList<ACharacterClass> GetPlayerTeamClassesInput(int teamSize)
        {
            List<ACharacterClass> playerTeam = new List<ACharacterClass>();
            for (int i = 0; i < teamSize; i++)
            {
                playerTeam.Add(GetPlayerClassInput());
            }


            return playerTeam;
        }

        ACharacterClass GetPlayerClassInput()
        {
            int input = -1;
            bool inputIsValid = false;

            IReadOnlyList<ACharacterClass> characterClasses = GameConstants.CHARACTER_CLASSES;

            while (!inputIsValid)
            {
                Console.WriteLine("Choose Between One of this Classes:" + Environment.NewLine);
                for (int j = 0; j < characterClasses.Count; j++)
                {
                    Console.Write($"[{j}] {characterClasses[j]} ");
                }
                Console.Write(Environment.NewLine);

                inputIsValid = TryReadInt(out input);

                inputIsValid &= input >= 0 & input < characterClasses.Count;

                if (!inputIsValid)
                {
                    Console.WriteLine($"ERROR: The class selected does not exist. Try Again.");
                }
            }

            ACharacterClass chosenClass = characterClasses[input];

            Console.WriteLine($"Player Class Choice: {chosenClass}");
            return chosenClass;
        }

        int GetPlayerTeamSizeInput(Size battlefieldSize)
        {
            int input = -1;

            bool inputIsValid = false;
            int maxTeamSize =  battlefieldSize.Area()/GameConstants.TEAMS.Count;

            while (!inputIsValid)
            {
                Console.WriteLine($"Choose team size (Max: {maxTeamSize})\n");

                inputIsValid = TryReadInt(out input);
                inputIsValid &= input > 0 || input <= maxTeamSize;

                if (!inputIsValid)
                {
                    Console.WriteLine($"ERROR: Team size must be positive number and less than " +
                        $"{maxTeamSize} to fit the battlefield. Try again");
                }
            }

            return input;
        }

        Size GetBattlefieldSizeInput()
        {
            Size battlefieldSize = default;

            bool inputIsValid = false;

            while (!inputIsValid)
            {
                Console.WriteLine("Choose the width of the battlefield.");
                inputIsValid = TryReadInt(out int widthInput);

                Console.WriteLine("Choose the height of the battlefield.");
                inputIsValid &= TryReadInt(out int heightInput);

                battlefieldSize.Width = widthInput;
                battlefieldSize.Height = heightInput;

                inputIsValid &= battlefieldSize.Area() > 2;

                if (!inputIsValid)
                {
                    Console.WriteLine($" ERROR: The battlefield need to have at least {Battlefield.MIN_AREA_SIZE}" +
                        $" of total area." + Environment.NewLine + $"Try Again." + Environment.NewLine);
                }

            }

            return battlefieldSize;
        }

        bool TryReadInt(out int value)
        {
            string input = Console.ReadLine();
            bool result = Int32.TryParse(input, out value);
            return result;
        }
    }
}

