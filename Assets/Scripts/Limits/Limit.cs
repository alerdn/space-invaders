using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.moveDirection *= -1;

            enemy.StepForward();
        }
    }
}
