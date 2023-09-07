using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  //引用库

public class PlayerController : MonoBehaviour
{
    [Header("基本参数")]
    public float speed = 260f;
    public float jumpForce = 16.5f;
    private PlayerInputControl inputControl;         //名字是脚本的名字
    private Vector2 inputDirection;
    private Rigidbody2D rb2D;
    private PhysicsCheck physicsCheck;
    public bool isHurt;
    public float hurtForce;
    public bool isDead = false;
    private PlayerAnimation playerAnimation;
    public bool isAttack;


    private void Awake()
    {
        //Awake主要是用于变量初始化
        rb2D = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();    //实例化这个变量
        playerAnimation = GetComponent<PlayerAnimation>();

        inputControl.Gameplay.Jump.started += Jump;//小闪电代表事件
        //+= 就是注册事件，把Jump这个函数注册到按下向跳跃键的时候
        //也就是按下跳跃键的那一刻就执行这个函数，可以通过快速修复来生成函数

        //攻击
        inputControl.Gameplay.Attack.started += PlayerAttack;
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        // 一直获取输入
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //刚体主要放到FixedUpdate中
        if (!isHurt)
            Move();
    }

    private void Move()
    {
        rb2D.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb2D.velocity.y);
        int faceDirection = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDirection = 1;
        if (inputDirection.x < 0)
            faceDirection = -1;
        //人物翻转
        transform.localScale = new Vector3(faceDirection, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();//删除这一行
        if (physicsCheck.isGround)
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
        playerAnimation.PlayerAttack();
        isAttack = true;
    }
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb2D.velocity = Vector2.zero;
        Vector2 forceDirection = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb2D.AddForce(forceDirection * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDie()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
}
