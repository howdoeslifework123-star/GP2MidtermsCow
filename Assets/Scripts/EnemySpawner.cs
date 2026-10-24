using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBase[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;  
    [SerializeField] private Transform player;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float groupSpread = 0.8f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < spawnInterval) return;
        timer = 0f;

        EnemyBase prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        
        for (int i = 0; i < prefab.GroupSize; i++)
        {
            Vector2 o = Random.insideUnitCircle * groupSpread;
            Vector3 pos = point.position + new Vector3(o.x, 0f, o.y);
            Instantiate(prefab, pos, Quaternion.identity).Init(player);
        }
    }
}