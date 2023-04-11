using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageable, IKillable
{
    public event Action<int> OnInit;
    public event Action<int> OnDamageTaken;
    public event Action OnKill;

    [Header("Setup")]
    [SerializeField] private int _maxLife = 5;
    [SerializeField] private int _currentLife;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 16;
    [SerializeField] private float _fireRate = .1f;

    [Header("Controls")]
    [SerializeField] private EventTrigger _attackBtn;
    [SerializeField] private Joystick _joystick;

    [Header("Player Model")]
    [SerializeField] private Transform model;
    [SerializeField] private float secondsToRotate = 0.5f;

    [Header("Bullet Pool")]
    [SerializeField] private BulletPoolManager poolManager;
    [SerializeField] private Transform shootPoint;

    private bool _isInvulnerable = false;
    private bool _tryingToShoot;
    private Coroutine _shooting = null;

    private void Start()
    {
        if (ShipSelector.Instance != null)
        {
            ScriptableShip ship = ShipSelector.Instance.SelectedShip;
            model = Instantiate(ship.Prefab.transform, transform);
            _maxLife = ship.MaxLife;
            _speed = ship.Speed;
            _damage = ship.Damage;
            _fireRate = 1f / (float)ship.FirePerSeconds;
        }
        OnInit?.Invoke(_maxLife);

        _currentLife = _maxLife;
        model.transform.localScale = .08f * Vector3.one;

        HandleTriggers();
    }

    private void HandleTriggers()
    {
        var onPointerDown = new EventTrigger.Entry();
        onPointerDown.eventID = EventTriggerType.PointerDown;
        onPointerDown.callback.AddListener((myEvent) => _tryingToShoot = true);

        var onPointerUp = new EventTrigger.Entry();
        onPointerUp.eventID = EventTriggerType.PointerUp;
        onPointerUp.callback.AddListener((myEvent) => _tryingToShoot = false);

        _attackBtn.triggers.Add(onPointerDown);
        _attackBtn.triggers.Add(onPointerUp);
    }

    private void Update()
    {
        // Key Down
        if (RightControl())
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.right);
            RotateShip(new Vector3(0, 0, -45));
        }
        else if (LeftControl())
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.left);
            RotateShip(new Vector3(0, 0, 45));
        }

        if (ShootControl())
        {
            if (_shooting == null)
                _shooting = StartCoroutine(Shoot());
        }

        // Key Up
        if (RightControl() || LeftControl())
        {
            RotateShip(Vector3.zero);
        }
    }

    private IEnumerator Shoot()
    {
        var bullet = poolManager.GetPooledBullet();
        if (bullet)
        {
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().StartBullet(_damage);
            bullet.transform.position = shootPoint.position;
        }

        yield return new WaitForSeconds(_fireRate);
        _shooting = null;
    }

    public void Damage(int damage)
    {
        if (!_isInvulnerable)
        {
            _currentLife -= damage;
            OnDamageTaken?.Invoke(_currentLife);

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
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || _joystick.Horizontal > 0;
    }

    private bool LeftControl()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || _joystick.Horizontal < 0;
    }

    private bool ShootControl()
    {
        return Input.GetKeyDown(KeyCode.Space) || _tryingToShoot;
    }

    private void RotateShip(Vector3 angle)
    {
        model
            .transform
            .DORotate(angle, secondsToRotate)
            .SetEase(Ease.OutBack);
    }
}