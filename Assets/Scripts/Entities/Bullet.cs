using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Entities;

public class Bullet : MonoBehaviour
{
    public float speed = 15;
    public float timeToReset = 4;

    public void StartBullet()
    {
        Invoke(nameof(FinishUsage), timeToReset);
    }

    private void FinishUsage()
    {
        gameObject.SetActive(false);
    }
}
