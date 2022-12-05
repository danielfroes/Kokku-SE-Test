namespace AutoBattle
{
    public interface IBattlefieldEntity
    {
        Position Position { get; set; }
        string DisplaySymbol { get; }
    }
}