using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystemActions : MonoBehaviour
{
    private ParticleSystemClass variables = new ParticleSystemClass();
    private ParticleSystem particleSys;                          /* 粒子系统 */
    private ParticleSystem.Particle[] particleArr;               /* 粒子 */
    private ParticalClass[] particles;                           /* 粒子属性 */

    // Start is called before the first frame update
    void Start()
    {
        particleArr = new ParticleSystem.Particle[variables.particleNum];
        particles = new ParticalClass[variables.particleNum];

        particleSys = this.GetComponent<ParticleSystem>();
        particleSys.startSpeed = 0;                              /* 粒子初始速度设为0，即保持在初始位置 */  
        particleSys.startSize = variables.particleSize;          /* 设置粒子大小 */ 
        particleSys.loop = false;                                /* 粒子发射到全部数目后，不从头开始 */
        particleSys.maxParticles = variables.particleNum;        /* 粒子数目 */  
        particleSys.Emit(variables.particleNum);                   
        particleSys.GetParticles(particleArr);                   /* 使用这个粒子数组作为buffer，获得当前活跃的粒子们 */

        for (int i = 0; i < variables.particleNum; ++i)
        {     
            float midRadius = (variables.maxRadius + variables.minRadius) / 2;                              /* 粒子游离的平均半径，即集中的位置 */
            float radio1 = Random.Range(1.0f, midRadius / variables.minRadius);                             /* 计算一个比率，使得半径更接近于midRadius */
            float radio2 = Random.Range(midRadius / variables.maxRadius, 1.0f);                             /* 计算一个比率，使得半径更接近于midRadius */
            float radius = Random.Range(variables.minRadius * radio1, variables.maxRadius * radio2);        /* 为粒子设定一个游离的半径 */

            float angle = Random.Range(0.0f, 360.0f);                                                       /* 随机获得一个角度制的角度 */
            float theta = angle / 180 * Mathf.PI;                                                           /* 换算为弧度值的角度 */
 
            float time = Random.Range(0.0f, 360.0f);                                                        

            particles[i] = new ParticalClass(radius, angle, time);                                          /* 随机获得粒子发射的时间 */

            particleArr[i].position = new Vector3(particles[i].radius * Mathf.Cos(theta), 0f, particles[i].radius * Mathf.Sin(theta));
        }

        particleSys.SetParticles(particleArr, particleArr.Length);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < variables.particleNum; i++)
        {
            /* 更新粒子的发射角度，使得其游离范围始终在光环内 */
            if (variables.clockwise)   
                particles[i].angle -= Time.deltaTime * variables.speed / 10;
            else             
                particles[i].angle += Time.deltaTime * variables.speed / 10;
            particles[i].angle = (360.0f + particles[i].angle) % 360.0f;                /* 保证angle小于360 */
            particles[i].time += Time.deltaTime;

            /* 粒子环绕的圆的半径按Time.deltaTime的一个比率的间隔值循环递增递减，像乒乓球一样来来回回 */
            particles[i].radius += Mathf.PingPong(particles[i].time / variables.minRadius / variables.maxRadius, variables.pingPong) - variables.pingPong / 2.0f;
            float theta = particles[i].angle / 180 * Mathf.PI;

            /* 以原点为圆心，游离半径为半径，画圆，作为位置 */
            particleArr[i].position = new Vector3(particles[i].radius * Mathf.Cos(theta), 0f, particles[i].radius * Mathf.Sin(theta));
        }
        changeColor();
        particleSys.SetParticles(particleArr, particleArr.Length);
    }

    void changeColor()
    {
        int pieces = 100;

        for (int i = 0; i < pieces; i++)
        {
            for (int j = 0; j < (int)(variables.particleNum / pieces); j++)
            {
                /* 计算当前不足1秒的时间零头 */
                float value = (Time.realtimeSinceStartup - Mathf.Floor(Time.realtimeSinceStartup));
                /* 计算粒子所在位置的内外层，要有渐变过程 */
                value += particles[(int)(i * (variables.particleNum / pieces) + j)].angle / 2 / Mathf.PI / 2;
                while (value > 1)
                    value--;
                particleArr[(int)(i * (variables.particleNum / pieces) + j)].color = variables.colorGradient.Evaluate(value);
            }
        }
    }
}
