namespace AutoBattle.CharacterActions
{
    public class FocusAction : ICharacterAction
    {
        const int ADDED_BUFF_STAGE = 2;
        public bool IsInRange(int targetDistance) => true;

        public string Execute(Character character, Character target, Battlefield battlefield)
        {
            target.BattleStats.AddAttackStage(ADDED_BUFF_STAGE);

            return $"{character} increased its focus, sharply increasing its attack stat.";
        }
    }
}