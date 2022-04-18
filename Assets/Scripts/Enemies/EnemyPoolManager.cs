using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Enemies;

public class EnemyPoolManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> enemyPool;
    public int amount = 3;

    [Header("Spawn")]
    public List<Transform> spawnPoints;
    public BulletPoolManager bulletPoolManager;

    private void Awake()
    {
        StartPool();
    }

    private void Update()
    {
        foreach (var spawn in spawnPoints)
        {
            GetShootingEnemy(spawn)?.GetComponent<Enemy>().CanShoot(true);
        }
    }

    private void StartPool()
    {
        enemyPool = new List<GameObject>();
        foreach (var spawn in spawnPoints)
        {
            for (int i = 0; i < amount; i++)
            {
                var enemy = Instantiate(enemyPrefab, spawn);
                enemy.transform.position = spawn.position + (Vector3.forward * 3 * i);

                // Adicionando o bullet pool manager
                enemy.GetComponent<Enemy>().poolManager = bulletPoolManager;

                enemyPool.Add(enemy);
            }
        }
    }

    public void RestartPool()
    {
        foreach (var enemy in enemyPool)
        {
            enemy.SetActive(true);
        }
    }

    private GameObject GetShootingEnemy(Transform spawn)
    {

        for (int i = 0; i < spawn.childCount; i++)
        {
            var enemy = spawn.GetChild(i);

            // Retorna o primeiro inimigo ativo da lista do spawn atual
            if (enemy.gameObject.activeInHierarchy) return enemy.gameObject;
        }

        return null;
    }

}
