public class Snake : EnemyBase
{
    protected override float MoveSpeed => 1.5f;
    protected override int MaxHealth => 2;   
    public override GameColor EnemyColor => GameColor.Green;
}