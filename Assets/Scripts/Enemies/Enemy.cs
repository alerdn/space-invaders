using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public int speedX = 2;
    public int speedZ = 3;
    public Vector3 moveDirection = Vector3.right;

    [Header("Bullet Pool")]
    public EnemyBulletPoolManager poolManager;
    public Transform shootPoint;
    public float intervalToShoot = 5f;

    private Coroutine _shooting = null;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void Update()
    {
        Move(moveDirection);
        if (_shooting == null) _shooting = StartCoroutine(Shoot());
    }

    public void StepForward()
    {
        transform.DOMoveZ(transform.position.z + -speedZ, 1f);
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * speedX * Time.deltaTime);
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

        Debug.Log("atirou");
        _shooting = null;
    }
}
