using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable, IKillable
{
    public event Action<int> OnInit;
    public event Action<int> OnDamageTaken;
    public event Action OnKill;

    [Header("Setup")]
    [SerializeField] private int _maxLife = 5;
    [SerializeField] private int _currentLife;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _secondsInvulnerable;
    [SerializeField] private float _speed = 16;

    [Header("Player Model")]
    [SerializeField] private Transform model;
    [SerializeField] private float secondsToRotate = 0.5f;
    [SerializeField] private Color _invulnerableColor;

    [Header("Bullet Pool")]
    [SerializeField] private BulletPoolManager poolManager;
    [SerializeField] private Transform shootPoint;

    private Vector3 _initialPosition;
    private bool _isInvulnerable = false;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _initialPosition = transform.position;

        if (ShipSelector.Instance != null)
        {
            ScriptableShip ship = ShipSelector.Instance.SelectedShip;
            model = Instantiate(ship.Prefab.transform, transform);
            _maxLife = ship.MaxLife;
            _speed = ship.Speed;
            _damage = ship.Damage;
        }
        OnInit?.Invoke(_maxLife);

        _currentLife = _maxLife;
        model.transform.localScale = .08f * Vector3.one;
        _meshRenderer = model.GetComponent<MeshRenderer>();

    }

    private void Update()
    {
        // Key Down
        if (RightControl())
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, -45));
        }
        else if (LeftControl())
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            RotateShip(new Vector3(0, 0, 45));
        }

        if (ShootControl())
        {
            var bullet = poolManager.GetPooledBullet();
            if (bullet)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().StartBullet(_damage);
                bullet.transform.position = shootPoint.position;
            }
        }

        // Key Up
        if (RightControl() || LeftControl())
        {
            RotateShip(Vector3.zero);
        }
    }

    public void Damage(int damage)
    {
        if (!_isInvulnerable)
        {
            _currentLife -= damage;
            OnDamageTaken?.Invoke(_currentLife);

            transform.position = _initialPosition;
            StartCoroutine(SetInvulnerable());

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

    private IEnumerator SetInvulnerable()
    {
        _isInvulnerable = true;
        _meshRenderer.material.SetColor("_Color", _invulnerableColor);
        yield return new WaitForSeconds(_secondsInvulnerable);
        _meshRenderer.material.SetColor("_Color", Color.white);
        _isInvulnerable = false;
    }
}