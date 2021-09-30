using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretHealth : MonoBehaviour
{
    [Tooltip("Maximum amount of health")] public float maxHealth = 10f; // 적 스택에 따라 다르게 분배한다

    [Tooltip("Health ratio ait which the critical health vignette starts appearing")]
    // 치명타 //
    public float CriticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> OnDamaged;
    public UnityAction<float> OnHealed;
    public UnityAction OnDie;


    public float currentHealth { get; set; }
    public bool Invincible { get; set; }

    public bool CanPickup() => currentHealth < maxHealth;
    public float GetRatio() => currentHealth / maxHealth;
    public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

    // 사망처리 //
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        // Hp 초기화
        currentHealth = maxHealth;
    }
    public void Heal(float healAmoundt)
    {
        // 힐 전 = 현재 Hp //
        float healthBefore = currentHealth;
        currentHealth += healAmoundt;
        // maxHp에서 더는 오르지 않는다 //
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // 힐 amount 반영 //
        float trueHealAmount = currentHealth - healthBefore;
        if(trueHealAmount > 0f)
        {
            OnHealed?.Invoke(trueHealAmount);
        }
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        if (Invincible) // 만약 무적 상태면
            return; // 그대로

        // 현 hp 상태 고정
        float healthBefore = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Damage action을 불러온다 //
        float trueDamageAmount = healthBefore - currentHealth;
        if(trueDamageAmount > 0f)
        {
            // 만약 OnDamaged null이면 
            OnDamaged?.Invoke(trueDamageAmount, damageSource);
        }
    }

    public void Kill()
    {
        currentHealth = 0f;

        // OnDamge action을 불러온다
        OnDamaged?.Invoke(maxHealth, null);

        HandleDeath();
    }

    void HandleDeath()
    {
        if (isDead)
            return;

        if(currentHealth <= 0f)
        {
            isDead = true;
            OnDie?.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
