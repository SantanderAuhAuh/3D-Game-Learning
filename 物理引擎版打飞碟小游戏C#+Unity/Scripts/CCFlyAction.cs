using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{
    float acceleration;
    float horizontalSpeed;
    Vector3 direction;
    float time;
    bool flag=false;                                           /* 记录游戏是否经过暂停状态 */
    Vector3 temp;                                        /* 记录游戏暂停时刚体游戏对象的速度矢量 */
    Rigidbody rigidbody;                                 /* 物理运动，添加刚体 */

    public static CCFlyAction getCCFlyAction()
    {
        CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
        return action;
    }

    public override void Start()
    {
        enable = true;
        acceleration = 9.8f;
        time = 0;
        horizontalSpeed = gameObject.GetComponent<DiskData>().getSpeed();
        direction = gameObject.GetComponent<DiskData>().getDirection();
        /* 完成行为的游戏对象如果有刚体性质，则需要设置刚体的速度属性 */
        rigidbody = gameObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.velocity = horizontalSpeed * direction;
            temp = rigidbody.velocity;                     /* temp的初始化 */
        }

    }

    public override void Update()
    {
        if (gameObject.activeSelf)
        {
            time += Time.deltaTime;
            transform.Translate(Vector3.down * acceleration * time * Time.deltaTime);
            transform.Translate(direction * horizontalSpeed * Time.deltaTime);
            if (this.transform.position.y < -4)
            {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
            }
        }
    }

    public override void FixedUpdate()               /* 刚体的场景更新函数，与Update的区别在于，不需要完成运动实现了 */
    {
        if (gameObject.activeSelf)
        {
            //Debug.Log(gameObject.GetComponent<Rigidbody>().useGravity);
            if (this.transform.position.y < -4)
            {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
            }
        }
    }

    public override void rigidbodyStopAction()
    {
        if (rigidbody)
        {
            if (!flag)                               /* 在Update中该赋值只进行一次 */
            {
                temp = rigidbody.velocity;                    
                flag = true;
            }
            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = false;
        }
    }

    public override void rigidbodyStartAction()
    {
        if (rigidbody)
        {
            Debug.Log(temp+" "+ rigidbody.velocity+" "+flag);
            rigidbody.useGravity = true;
            if (flag)                                /* 在Update中该赋值只进行一次 */
            {
                rigidbody.velocity = temp;               
                flag = false;
            }
        }
    }
}
