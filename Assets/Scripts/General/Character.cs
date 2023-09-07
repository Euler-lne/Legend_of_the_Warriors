using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float curHealth;
    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    public UnityEvent<Transform> onTakeDamage;
    public UnityEvent onDie;
    private void Start()
    {
        curHealth = maxHealth;
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TackDamage(Attack attacker)
    {
        if (invulnerable)
            return;
        if (curHealth - attacker.damage > 0)
        {
            curHealth -= attacker.damage;
            TirggerInvulnerable();
            //执行受伤
            onTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            curHealth = 0;
            onDie?.Invoke();
        }
    }
    /// <summary>
    /// 触发无敌
    /// </summary>
    private void TirggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
