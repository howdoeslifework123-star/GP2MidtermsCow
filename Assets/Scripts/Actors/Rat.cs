public class Rat : EnemyBase
{
    protected override float MoveSpeed => 3f;  
    protected override int MaxHealth => 1;
    public override GameColor EnemyColor => GameColor.Gray;
}