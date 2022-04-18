using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Entities;

public class PlayerBullet : Bullet
{
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.GetContact(0).otherCollider;
        obj.GetComponent<Enemy>()?.Kill();

        // Desativando bullet ao acertar inimigo
        gameObject.SetActive(false);
    }
}
