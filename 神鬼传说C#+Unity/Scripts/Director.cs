using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneController;

namespace Director
{
    public class SSDirector : System.Object
    {
        /* 单例模式，导演只有一个 */
        private static SSDirector _instance;
        /* 记录当前的场记，也即记录了当前场景 */
        public ISceneController currentSceneController { get; set; }
        /* 记录游戏是否正在运行 */
        public bool running { get; set; }

        /* 获得单例 */
        public static SSDirector getInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }
    }
}