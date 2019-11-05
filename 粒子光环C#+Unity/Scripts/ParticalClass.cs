using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalClass
{
    public float radius = 0f, angle = 0f, time = 0f;                    /* 粒子游离的半径，发射的角度、时间 */
    public ParticalClass(float radius, float angle, float time)
    {
        this.radius = radius;
        this.angle = angle;
        this.time = time;
    } 
}
