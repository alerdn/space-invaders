using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 5;
    public float timeToReset = 10;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public void StartBullet()
    {
        Invoke(nameof(FinishUsage), timeToReset);
    }

    private void FinishUsage()
    {
        gameObject.SetActive(false);
    }
}
