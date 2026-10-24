using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 4f;

    public GameColor BulletColor { get; private set; }

    public void Init(GameColor color)
    {
        BulletColor = color;
        gameObject.Tint(color.ToColor());
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponentInParent<IDamageable>();
        if (target == null) return;

        target.TakeDamage(BulletColor); 
        Destroy(gameObject);     
    }
}