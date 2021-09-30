using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretHealth : MonoBehaviour
{
    [Tooltip("Maximum amount of health")] public float maxHealth = 10f; // �� ���ÿ� ���� �ٸ��� �й��Ѵ�

    [Tooltip("Health ratio ait which the critical health vignette starts appearing")]
    // ġ��Ÿ //
    public float CriticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> OnDamaged;
    public UnityAction<float> OnHealed;
    public UnityAction OnDie;


    public float currentHealth { get; set; }
    public bool Invincible { get; set; }

    public bool CanPickup() => currentHealth < maxHealth;
    public float GetRatio() => currentHealth / maxHealth;
    public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

    // ���ó�� //
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        // Hp �ʱ�ȭ
        currentHealth = maxHealth;
    }
    public void Heal(float healAmoundt)
    {
        // �� �� = ���� Hp //
        float healthBefore = currentHealth;
        currentHealth += healAmoundt;
        // maxHp���� ���� ������ �ʴ´� //
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // �� amount �ݿ� //
        float trueHealAmount = currentHealth - healthBefore;
        if(trueHealAmount > 0f)
        {
            OnHealed?.Invoke(trueHealAmount);
        }
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        if (Invincible) // ���� ���� ���¸�
            return; // �״��

        // �� hp ���� ����
        float healthBefore = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Damage action�� �ҷ��´� //
        float trueDamageAmount = healthBefore - currentHealth;
        if(trueDamageAmount > 0f)
        {
            // ���� OnDamaged null�̸� 
            OnDamaged?.Invoke(trueDamageAmount, damageSource);
        }
    }

    public void Kill()
    {
        currentHealth = 0f;

        // OnDamge action�� �ҷ��´�
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
