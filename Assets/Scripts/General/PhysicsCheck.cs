using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("状态")]
    public bool isGround;
    [Header("检测参数")]
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;    //位移差值检测
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        // 检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        // 强制类型转换
    }

    // private void OnDrawGizmosSelected()
    // {
    //     //选中的时候绘制
    //     //绘制一个空心球
    //     Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    // }
}
