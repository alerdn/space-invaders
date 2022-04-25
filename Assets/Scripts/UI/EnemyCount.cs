using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCount : MonoBehaviour
{
    [Header("Enemy Counter")]
    public TMP_Text enemyCounterText;
    public EnemyPoolManager enemyPoolManager;

    private void Start()
    {
        UpdateEnemyCounter(0);
    }

    void Update()
    {
        UpdateEnemyCounter(enemyPoolManager.GetEnemiesDestroyedAmount());
    }

    private void UpdateEnemyCounter(int count)
    {
        enemyCounterText.text = $"Invasores destruídos: {count}/{enemyPoolManager.enemyPool.Count}";
    }
}
