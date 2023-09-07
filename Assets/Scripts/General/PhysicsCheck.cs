using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("状态")]
    public bool isGround;
    [Header("检测参数")]
    public bool manual;
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;    //位移差值检测

    public bool touchLeftWall;
    public Vector2 leftOffset;
    public bool touchRightWall;
    public Vector2 rightOffset;

    private void Awake() {
        coll = GetComponent<CapsuleCollider2D>();
        if(!manual)
        {
            // bounds代表的是世界坐标
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-(coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
        }
    }
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        // 检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        // 强制类型转换

        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);

    }

    // private void OnDrawGizmosSelected()
    // {
    //     //选中的时候绘制
    //     //绘制一个空心球
    //     Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    //     Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
    //     Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    // }
}
