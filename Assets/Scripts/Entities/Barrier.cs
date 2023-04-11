using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IKillable, IDamageable
{
    [Header("Colors")]
    [SerializeField] [ColorUsage(true, true)] private Color _fullLifeColor;
    [SerializeField] [ColorUsage(true, true)] private Color _lightlyHitLifeColor;
    [SerializeField] [ColorUsage(true, true)] private Color _mediumLifeColor;
    [SerializeField] [ColorUsage(true, true)] private Color _criticalLifeColor;

    [Header("Life")]
    [SerializeField] private float maxLife = 5f;
    [SerializeField] private float _currentLife;

    private MeshRenderer mesh;

    private void Awake()
    {
        _currentLife = maxLife;
        mesh = gameObject.GetComponentInChildren<MeshRenderer>();
        SetColor(_fullLifeColor);
    }

    public void Damage(int damage)
    {
        _currentLife -= damage;

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
        DOTween.To(() => mesh.material.GetColor("_dotsColor"), color => mesh.material.SetColor("_dotsColor", color), c, 1f);
    }

    private void ChangeColor(float lifePercentage)
    {
        if (lifePercentage <= 0.25f)
            SetColor(_criticalLifeColor);
        else if (lifePercentage <= 0.5f)
            SetColor(_mediumLifeColor);
        else if (lifePercentage <= 0.75f)
            SetColor(_lightlyHitLifeColor);
    }
}
