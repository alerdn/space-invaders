using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLine : MonoBehaviour
{
    public float speedX = 8.5f;
    public Vector3 moveDirection = Vector3.right;

    private void Update()
    {
        Move(moveDirection);
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * speedX * Time.deltaTime);
    }
}
