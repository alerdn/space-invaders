using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IKillable, IDamageable
{
    public float maxLife = 5f;

    [SerializeField]
    private float _currentLife;
    private MeshRenderer mesh;

    private void Awake()
    {
        _currentLife = maxLife;
        mesh = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    public void Damage()
    {
        _currentLife--;

        float lifePercentage = LifePercentage(_currentLife);
        ChangeColor(lifePercentage);

        if (_currentLife <= 0) Kill();
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    private float LifePercentage(float value)
    {
        return value / maxLife;
    }

    private void SetColor(Color c)
    {
        mesh.material.SetColor("_Color", c);
    }

    private void ChangeColor(float lifePercentage)
    {
        if (lifePercentage <= 0.25f)
            SetColor(Color.red);
        else if (lifePercentage <= 0.5f)
            SetColor(Color.yellow);
        else if (lifePercentage <= 0.75f)
            SetColor(Color.green);
    }
}
