using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 场记命名空间，便于被Director.cs文件中引用，场记应该受导演管制和调用 */

namespace SceneController {
    /* 场记接口:用于被场记类继承，一个游戏中通常不止一个场记 */
    public interface ISceneController
    {
        void LoadResource();                     /* 加载当前场景的资源 */
        //void Pause();                          /* 当前场景暂停 */
        //void Resume();                         /* 当前场景继续进行 */
    }
}
