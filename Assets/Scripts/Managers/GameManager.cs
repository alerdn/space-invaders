using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Entities")]
    [SerializeField] private Player _player;

    [Header("UI")]
    [SerializeField] private GameObject _endScreen;

    private void Start()
    {
        _player.OnKill += EndGame;
    }

    private void EndGame()
    {
        _endScreen.SetActive(true);
    }
}
