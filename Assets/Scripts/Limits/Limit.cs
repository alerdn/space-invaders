using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
