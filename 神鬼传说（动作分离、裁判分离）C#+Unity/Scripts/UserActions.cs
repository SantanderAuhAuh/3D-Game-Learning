using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Director;
using Models;

namespace UserActions
{
    public interface IUserAction
    {
        void MoveBoat();                       /* 小船向另一边航行，条件：小船已靠岸 ，船上有人物 */
        void MoveRole(Role role);              /* 牧师或恶魔上船或下船，条件：船不在移动且两者在同一侧 */
        //bool Check();                          /* 游戏结束，条件：岸上牧师的人数小于恶魔数; 通关，条件：所有人物都达到河岸另一边 */
        void Resume();                         /* 游戏重新开始 */
    }

}
