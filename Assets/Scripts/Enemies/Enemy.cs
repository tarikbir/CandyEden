using System;
using UnityEngine;
using UnityTimer;

public class Enemy : Billboard
{
    [Header("Info")]
    public string Name;
    public float HealthMax;
    public float SugarValue;
    public bool IsFinalBoss;

    [Header("Combat")]
    public float Health;
    public float AttackDamage;
    public float AttackInterval;
    public bool Raged;

    [SerializeField] private float _animationSpeed = 0.6f;
    [SerializeField] private Sprite[] _normalAnimationFrames;
    [SerializeField] private Sprite[] _enrageAnimationFrames;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private Projectile CandyPrefab;

    private int _animationIndex;
    private Timer _animationTimer;
    private Timer _attackTimer;
    private Timer _damageVFXTimer;

    protected override void Start()
    {
        base.Start();
        var canvas = _healthBar.GetComponentInParent<Canvas>();
        canvas.worldCamera = GameManager.Instance.MainCamera;
        _healthBar.Setup(Health, HealthMax);
        _animationTimer = Timer.Register(_animationSpeed, Animate, isLooped: true, autoDestroyOwner: this);
        _attackTimer = Timer.Register(AttackInterval, Attack, isLooped: true, autoDestroyOwner: this);
        _animationIndex = 0;
    }

    public void ShittedOn(float timer)
    {
        _attackTimer.Pause();
        Debug.Log($"{name} got shit on");
        Timer.Register(timer, ShitComplete, autoDestroyOwner: this);
    }

    private void ShitComplete()
    {
        Debug.Log($"{name} got shit cleared");
        _attackTimer.Resume();
    }

    public void Die()
    {
        _damageVFXTimer.Cancel();
        _animationTimer.Cancel();
        _attackTimer.Cancel();

        WaveControl.Instance.CurrentMonsters.Remove(this);

        if (IsFinalBoss)
        {
            GameManager.Instance.WinGame();
        }

        Destroy(gameObject);
    }

    public bool GetDamage(float damage)
    {
        StartDamageFX();

        Health -= damage;

        _healthBar.Current = Health;

        if (Health <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    private void Attack()
    {
        if (GameManager.Instance.Settings.EnableThrowingCandies)
        {
            var candy = Instantiate(CandyPrefab, transform.position, Quaternion.identity);
            candy.Direction = GameManager.Instance.MainCamera.transform.position - transform.position;
            candy.Damage = AttackDamage;
        }
        else
        {
            PlayerControl.Instance.AddHealth(-AttackDamage);
        }
    }

    private void Animate()
    {
        _animationIndex = ++_animationIndex % 2;

        if (Raged)
        {
            _spriteRenderer.sprite = _enrageAnimationFrames[_animationIndex];
            //TODO: Hizlandirma
        }
        else
        {
            _spriteRenderer.sprite = _normalAnimationFrames[_animationIndex];
        }
    }

    private void StartDamageFX()
    {
        if (_damageVFXTimer != null)
        {
            _damageVFXTimer.Cancel();
        }

        _spriteRenderer.color = Color.red;
        _damageVFXTimer = Timer.Register(2.5f, null, DamageFXTick);
    }

    private void DamageFXTick(float time)
    {
        _spriteRenderer.color += Color.white * time * Time.deltaTime;
    }
}