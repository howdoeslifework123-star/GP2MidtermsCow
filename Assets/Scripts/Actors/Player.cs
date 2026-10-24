using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ISwitchable
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Renderer[] tintTargets;  
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float range = 8f;
    [SerializeField] private float rotateSpeed = 360f;   
    [SerializeField] private LayerMask enemyMask;

    private GameColor currentColor = GameColor.Yellow;
    private float timer;

    private void Awake() => ApplyColor();

    private void Update()
    {
        HandleInput();
        RotateToNearestEnemy();

        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            timer = 0f;
            Shoot();
        }
    }

    private void HandleInput()
    {
        // Spacebar
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Switch();
            return;
        }

        // Click on the cow
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, ~0, QueryTriggerInteraction.Ignore)
            && hit.collider.GetComponentInParent<ISwitchable>() is ISwitchable s)
        {
            s.Switch();
        }
    }

    
    public void Switch()
    {
        currentColor = (GameColor)(((int)currentColor + 1) % 3);
        ApplyColor();
    }

    private void ApplyColor()
    {
        Color c = currentColor.ToColor();
        if (tintTargets == null || tintTargets.Length == 0)
            gameObject.Tint(c);
        else
            foreach (var r in tintTargets) r.Tint(c);
    }

    private void RotateToNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position, range, enemyMask, QueryTriggerInteraction.Collide);
        if (hits.Length == 0) return;

        Transform nearest = null;
        float best = float.MaxValue;
        foreach (var h in hits)
        {
            Vector3 d = h.transform.position - transform.position;
            d.y = 0f;
            if (d.sqrMagnitude < best) { best = d.sqrMagnitude; nearest = h.transform; }
        }

        Vector3 dir = nearest.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion target = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, target, rotateSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        Bullet b = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        b.Init(currentColor);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}