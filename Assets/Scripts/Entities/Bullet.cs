using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float speed = 45;
    protected float timeToReset = 10;
    protected int _damage;

    public void StartBullet(int damage)
    {
        _damage = damage;
        Invoke(nameof(FinishUsage), timeToReset);
    }

    private void FinishUsage()
    {
        gameObject.SetActive(false);
    }
}
