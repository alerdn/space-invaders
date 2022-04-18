using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    [Header("Bullet Pool")]
    public BulletPoolManager poolManager;
    public Transform shootPoint;
    [Header("Range interval to shoot")]
    public float minInterval = 1f;
    public float maxInterval = 5f;

    private float _intervalToShoot = 5f;
    private Coroutine _shooting = null;
    private bool canShoot = false;

    private void Update()
    {
        if (_shooting == null && canShoot)
        {
            _intervalToShoot = Random.Range(minInterval, maxInterval);
            _shooting = StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_intervalToShoot);

        var bullet = poolManager.GetPooledBullet();
        if (bullet)
        {
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().StartBullet();
            bullet.transform.position = shootPoint.position;
        }

        _shooting = null;
    }

    public void CanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
