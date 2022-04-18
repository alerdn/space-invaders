using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpaceInvaders.Enemies;

namespace SpaceInvaders.Limits;

public class Limit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            var enemyLine = collision.gameObject.GetComponent<EnemyLine>();
            enemyLine.moveDirection *= -1;
        }
    }
}
