namespace AutoBattle.CharacterActions
{
    public interface ICharacterAction
    {
        int Range { get; }

        string Execute(Character character, Character target, Battlefield battlefield);
    }


    //Paladin = 1,
    //Warrior = 2,
    //Cleric = 3,
    //Archer = 4

}