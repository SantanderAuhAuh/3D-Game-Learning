using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemClass
{
    public int particleNum = 300000;                               /* 粒子数目 */
    public float particleSize = 0.06f;                             /* 粒子大小 */
    public float minRadius = 5.0f;                                 /* 粒子运动的最小范围 */
    public float maxRadius = 12.0f;                                /* 粒子运动的最大范围 */
    public float speed = 2f;                                       /* 粒子运动速度 */
    public bool clockwise = true;
    public float pingPong = 0.02f;
    public Gradient colorGradient = new Gradient();                /* 光环颜色的渐变 */

    public ParticleSystemClass()
    {
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        colorGradient.SetKeys(colorKey, alphaKey);
    }
}
