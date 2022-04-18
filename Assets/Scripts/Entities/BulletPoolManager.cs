using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Entities;

public class BulletPoolManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> bulletPool;
    public int amount = 20;

    private void Awake()
    {
        StartPool();
    }

    private void StartPool()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            var obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(false);

            bulletPool.Add(obj);
        }
    }

    public GameObject GetPooledBullet()
    {
        foreach (var bullet in bulletPool)
            if (!bullet.activeInHierarchy) return bullet;

        return null;
    }
}
