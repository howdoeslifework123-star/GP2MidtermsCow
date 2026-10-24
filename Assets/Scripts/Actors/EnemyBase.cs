using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject deathEffect;  
    [SerializeField] private bool tintModel = true;   

    // Each enemy type defines its own stats and color
    protected abstract float MoveSpeed { get; }
    protected abstract int MaxHealth { get; }
    public abstract GameColor EnemyColor { get; }    
    public virtual int GroupSize => 1;      

    protected Transform target;
    private int health;

    protected virtual void Awake() => health = MaxHealth;

    private void Start()
    {
        if (tintModel) gameObject.Tint(EnemyColor.ToColor());
    }

    public void Init(Transform player) => target = player;

   
    protected virtual void Update()
    {
        if (target == null) return;

        Vector3 goal = target.position;
        goal.y = transform.position.y;  

        Vector3 dir = goal - transform.position;
        if (dir.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(dir);

        transform.position = Vector3.MoveTowards(
            transform.position, goal, MoveSpeed * Time.deltaTime);
    }

    // IDamageable
    public virtual void TakeDamage(GameColor bulletColor)
    {
        if (bulletColor != EnemyColor) return; 

        health--;
        if (health <= 0) Die();
    }

    protected virtual void Die()
    {
        GameManager.Instance.AddKill();
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.GameOver();
    }
}