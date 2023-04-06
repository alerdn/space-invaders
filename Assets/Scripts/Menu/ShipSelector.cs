using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelector : PersistentSingleton<ShipSelector>
{
    public ScriptableShip SelectedShip { get; private set; }

    public void SelectShip(ScriptableShip ship)
    {
        SelectedShip = ship;
    }
}
