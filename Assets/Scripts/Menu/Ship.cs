using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ScriptableShip Data { get => _data; }

    [SerializeField] private ScriptableShip _data;
}
