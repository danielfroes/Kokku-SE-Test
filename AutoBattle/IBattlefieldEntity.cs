namespace AutoBattle
{
    public interface IBattlefieldEntity
    {
        GridCell Position { get; set; }
        string DisplaySymbol { get; }
    }
}