using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.GetContact(0).otherCollider;

        obj.GetComponent<IDamageable>()?.Damage();

        // Desativando bullet ao acertar inimigo
        gameObject.SetActive(false);
    }
}
