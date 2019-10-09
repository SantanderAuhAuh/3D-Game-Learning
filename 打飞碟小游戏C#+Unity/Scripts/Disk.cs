using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiskClass{
    public class Disk : MonoBehaviour
    {
        public int value=1;                /* 击中得分 */
        public bool id;                  /* 飞碟身份，红色击中5次后游戏结束 */
        public float speed;              /* 飞碟运动速度 */
        public Vector3 direction;        /* 飞碟运动方向 */
        public Color color;
    }
}


