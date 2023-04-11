using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Setup")]
    [SerializeField] private int _maxLife = 1;
    [SerializeField] private int _currentLife;

    [Header("Bullet Pool")]
    public BulletPoolManager poolManager;
    public Transform shootPoint;
    [Header("Range interval to shoot")]
    public float minInterval = 1f;
    public float maxInterval = 3f;

    private float _intervalToShoot = 3f;
    private Coroutine _shooting = null;
    private bool canShoot = false;

    private void Start()
    {
        _currentLife = _maxLife;
    }

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
            bullet.GetComponent<Bullet>().StartBullet(1);
            bullet.transform.position = shootPoint.position;
        }

        _shooting = null;
    }

    public void CanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }

    public void Damage(int damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
            gameObject.SetActive(false);
    }
}
