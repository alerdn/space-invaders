using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 moveDirection;

    void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime);
    }
}
