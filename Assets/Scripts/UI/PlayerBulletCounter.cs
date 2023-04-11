using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBulletCounter : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private BulletPoolManager _playerBulletPoolManager;

    private void Update()
    {
        float ammoLeft = (float)_playerBulletPoolManager.AmmoLeft() / (float)_playerBulletPoolManager.amount * 100 * 256 / 100;
        _bar.rectTransform.sizeDelta = new Vector2(ammoLeft, 72f);
    }
}
