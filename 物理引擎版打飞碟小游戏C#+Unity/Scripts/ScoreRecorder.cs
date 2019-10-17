using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class ScoreRecorder : MonoBehaviour
    {
        public int score;
        private Dictionary<Color, int> scoreTable = new Dictionary<Color, int>();

        void Start()                                        /* 初始化各种颜色的分数值 */
        {
            score = 0;
            scoreTable.Add(Color.white, 1);
            scoreTable.Add(Color.gray, 2);
            scoreTable.Add(Color.black, 4);
        }

        public void reset()
        {
            score = 0;
        }

        public void record(GameObject disk)                 /* 击中时触发该函数，加分 */
        {
            score += scoreTable[disk.GetComponent<DiskData>().getColor()];
        }
    }
}