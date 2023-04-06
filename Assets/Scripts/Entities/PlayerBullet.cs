using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;

        obj.GetComponent<Enemy>()?.Damage(_damage);

        // Desativando a bullet ao acertar o jogador ou barreira
        gameObject.SetActive(false);
    }
}
