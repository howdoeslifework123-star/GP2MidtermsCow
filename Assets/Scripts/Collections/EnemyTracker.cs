using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI enemyListText;

    [Header("Spawning")]
    [SerializeField] private string[] enemyNames = { "DiscordMod", "Redditor", "Weeb", "Looksmaxxer", "Rotmaxxer" };

   
    private List<string> activeEnemies = new List<string>();

    private int spawnCounter = 0;

    private void Start()
    {
        RefreshUI();
    }

  
    public void SpawnEnemy()
    {
        spawnCounter++;
        string baseName = enemyNames[Random.Range(0, enemyNames.Length)];
        string enemyName = $"{baseName} #{spawnCounter}";

        AddTarget(enemyName);
    }

    public void AddTarget(string enemyName)
    {
        if (string.IsNullOrEmpty(enemyName)) return;

        activeEnemies.Add(enemyName);
        Debug.Log($"Spawned: {enemyName}");
        RefreshUI();
    }

  
    public void DefeatEnemy(string enemyName)
    {
        if (activeEnemies.Contains(enemyName))
        {
            activeEnemies.Remove(enemyName);
            Debug.Log($"Reformed: {enemyName}");
            RefreshUI();
        }
        else
        {
            Debug.LogWarning($"DefeatEnemy: '{enemyName}' is not in the active list.");
        }
    }

  
    public void DefeatNextEnemy()
    {
        if (activeEnemies.Count == 0)
        {
            Debug.LogWarning("DefeatNextEnemy: no enemies to defeat.");
            return;
        }

        DefeatEnemy(activeEnemies[0]);
    }

    
    public void WipeWave()
    {
        activeEnemies.Clear();
        spawnCounter = 0;
        Debug.Log("Incels wiped.");
        RefreshUI();
    }

    // Appends are nice
    private void RefreshUI()
    {
        if (enemyListText == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Active Enemies: {activeEnemies.Count}");
        sb.AppendLine("----------------");

        for (int n = 0; n < activeEnemies.Count; n++)
        {
            sb.AppendLine($"{n + 1}. {activeEnemies[n]}");
        }

        if (activeEnemies.Count == 0)
        {
            sb.AppendLine("(No Incels Spawned)");
        }

        enemyListText.text = sb.ToString();
    }
}