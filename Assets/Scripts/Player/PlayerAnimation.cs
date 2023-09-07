using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rd2D;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rd2D = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        // 除了刚体其他多可以放到Update中
        SetAnimation();
    }
    public void SetAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rd2D.velocity.x));
        animator.SetFloat("velocityY", rd2D.velocity.y);
        animator.SetBool("isGround", physicsCheck.isGround);
        animator.SetBool("isDie", playerController.isDead);
        animator.SetBool("isAttack", playerController.isAttack);
    }
    public void PlayerHurt()
    {
        animator.SetTrigger("hurt");
    }

    public void PlayerAttack()
    {
        animator.SetTrigger("attack");
    }
}
