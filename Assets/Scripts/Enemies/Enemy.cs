using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    [Header("Bullet Pool")]
    public EnemyBulletPoolManager poolManager;
    public Transform shootPoint;
    public float intervalToShoot = 5f;

    private Coroutine _shooting = null;
    private bool canShoot = false;

    private void Update()
    {
        if (_shooting == null && canShoot)
        {
            intervalToShoot = Random.Range(0.5f, 2);
            _shooting = StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(intervalToShoot);

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
        Destroy(gameObject);
    }
}
