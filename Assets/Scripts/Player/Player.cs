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

    private Tween _shipRotating;

    private void Update()
    {
        // Key Down
        if (RightControl())
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, -45));
        }
        else if (LeftControl())
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, 45));
        }

        if (ShootControl())
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
        if (RightControl() || LeftControl())
        {
            RotateShip(Vector3.zero);
        }
    }

    private bool RightControl()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    private bool LeftControl()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private bool ShootControl()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void RotateShip(Vector3 angle)
    {
        _shipRotating = model
            .transform
            .DORotate(angle, secondsToRotate)
            .SetEase(Ease.OutBack);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}