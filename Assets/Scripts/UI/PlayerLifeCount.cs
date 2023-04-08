using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeCount : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _lifePrefab;
    [SerializeField] private List<Image> _lifeTicks;

    private void Start()
    {
        _player.OnInit += Init;
        _player.OnDamageTaken += OnDamage;
    }

    private void Init(int maxLife)
    {
        _lifeTicks = new List<Image>();

        for (int i = 0; i < maxLife; i++)
        {
            _lifeTicks.Add(Instantiate(_lifePrefab.GetComponent<Image>(), transform));
        }
    }

    private void OnDamage(int currentLife)
    {
        for (int i = _lifeTicks.Count; i > currentLife; i--)
        {
            _lifeTicks[i-1].enabled = false;
        }
    }


}
