using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;
    private void OnTriggerStay2D(Collider2D other)
    {
        //语法糖是否有组件，有才执行
        other.GetComponent<Character>()?.TackDamage(this);
    }
}
