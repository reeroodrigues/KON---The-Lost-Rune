using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uiGunUpdater;

    public float damageMultiplier = 1;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }
    public virtual void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float f)
    {
        _currentLife -= f * damageMultiplier;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(uiGunUpdater != null)
        {
            uiGunUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damage, float duration)
    {
        StartCoroutine(ChangeDamageMultiplyCoroutine(damageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiplier, float duration)
    {
        this.damageMultiplier = damageMultiplier;
        yield return new WaitForSeconds(duration);
        this.damageMultiplier = 1;
    }
}
