using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Enemies;

public class EnemyLine : MonoBehaviour
{
    public int speedX = 2;
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
