using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship", fileName = "New Ship")]
public class ScriptableShip : ScriptableObject
{
    public string ShipName;
    public GameObject Prefab;
    public int MaxLife;
    public int Speed;
    public int Damage;
}
