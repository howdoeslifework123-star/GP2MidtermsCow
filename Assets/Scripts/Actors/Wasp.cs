public class Wasp : EnemyBase
{
    protected override float MoveSpeed => 2f;
    protected override int MaxHealth => 1;
    public override GameColor EnemyColor => GameColor.Yellow;
    public override int GroupSize => 3; 
}