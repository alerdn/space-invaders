using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour, IKillable
{
    public float speed = 16;

    [Header("Player Model")]
    public Transform model;
    public float secondsToRotate = 0.5f;

    [Header("Bullet Pool")]
    public BulletPoolManager poolManager;
    public Transform shootPoint;

    private void Update()
    {
        // Key Down
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, -45));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, 45));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = poolManager.GetPooledBullet();
            if (bullet)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().StartBullet();
                bullet.transform.position = shootPoint.position;
            }
        }

        // Key Up
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            RotateShip(Vector3.zero);
        }
    }

    private void RotateShip(Vector3 angle)
    {
        model
            .transform
            .DORotate(angle, secondsToRotate)
            .SetEase(Ease.OutBack);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}