using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;

        obj.GetComponent<Player>()?.Kill();
        obj.GetComponent<Barrier>()?.Damage();

        // Desativando a bullet ao acertar o jogador ou barreira
        gameObject.SetActive(false);
    }
}
