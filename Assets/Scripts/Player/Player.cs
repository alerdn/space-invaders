using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable, IKillable
{
    public event Action OnKill;

    [Header("Setup")]
    [SerializeField] private int _maxLife = 5;
    [SerializeField] private int _currentLife;
    [SerializeField] private float _secondsInvulnerable;
    [SerializeField] private float speed = 16;

    [Header("Player Model")]
    [SerializeField] private Transform model;
    [SerializeField] private float secondsToRotate = 0.5f;
    [SerializeField] private Color _invulnerableColor;

    [Header("Life Controller")]
    [SerializeField] private List<Image> _lifeTicks;

    [Header("Bullet Pool")]
    [SerializeField] private BulletPoolManager poolManager;
    [SerializeField] private Transform shootPoint;

    private Vector3 _initialPosition;
    private bool _isInvulnerable = false;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _currentLife = _maxLife;
        _initialPosition = transform.position;
        _meshRenderer = model.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Key Down
        if (RightControl())
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, -45));
        }
        else if (LeftControl())
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, 45));
        }

        if (ShootControl())
        {
            var bullet = poolManager.GetPooledBullet();
            if (bullet)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().StartBullet();
                bullet.transform.position = shootPoint.position;
            }
        }

        // Key Up
        if (RightControl() || LeftControl())
        {
            RotateShip(Vector3.zero);
        }
    }

    public void Damage()
    {
        if (!_isInvulnerable)
        {
            _currentLife--;
            _lifeTicks[_currentLife].gameObject.SetActive(false);

            OnDamage();

            if (_currentLife <= 0) Kill();
        }
    }

    public void Kill()
    {
        OnKill?.Invoke();
        gameObject.SetActive(false);
    }

    private bool RightControl()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    private bool LeftControl()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private bool ShootControl()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void RotateShip(Vector3 angle)
    {
        model
            .transform
            .DORotate(angle, secondsToRotate)
            .SetEase(Ease.OutBack);
    }

    private void OnDamage()
    {
        transform.position = _initialPosition;
        StartCoroutine(SetInvulnerable());
    }

    private IEnumerator SetInvulnerable()
    {
        _isInvulnerable = true;
        _meshRenderer.material.SetColor("_Color", _invulnerableColor);
        yield return new WaitForSeconds(_secondsInvulnerable);
        _meshRenderer.material.SetColor("_Color", Color.white);
        _isInvulnerable = false;
    }
}