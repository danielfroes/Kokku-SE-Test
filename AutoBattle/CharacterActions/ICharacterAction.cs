namespace AutoBattle.CharacterActions
{

    public interface ICharacterAction
    {
        string Execute(Character character, Character target, Battlefield battlefield);
        bool IsInRange(int targetDistance);
    }


    //Paladin = 1,
    //Warrior = 2,
    //Cleric = 3,
    //Archer = 4

}