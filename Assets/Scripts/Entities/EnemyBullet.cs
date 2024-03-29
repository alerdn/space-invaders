using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;

        obj.GetComponent<IDamageable>()?.Damage(_damage);

        // Desativando a bullet ao acertar o jogador ou barreira
        gameObject.SetActive(false);
    }
}
