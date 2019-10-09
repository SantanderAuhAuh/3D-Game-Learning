using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserActions;
using Director;

namespace GameView
{
    public class pageImage : MonoBehaviour
    {
        IUserAction userAction;
        
        public bool isOver = true;

        // Start is called before the first frame update
        void Start()
        {
            userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        }

        private void OnGUI()
        {
            if (!isOver)//游戏正在进行中
            {
                /* 点击事件 */
                if (Input.GetButtonDown("Fire1"))
                {
                    Vector3 pos = Input.mousePosition;
                    userAction.click(pos);
                }

                /* 分数和生命值标识 */
                GUI.color = Color.red;
                GUI.Label(new Rect(2 * Screen.width / 3, 0, 400, 400), "Score: " + userAction.getScore().ToString());
                GUI.Label(new Rect(Screen.width-120, 0, 400,400), "Life:" + userAction.getLife().ToString());


                GUI.color = Color.red;
                if (userAction.getLife() <= 0)//游戏失败
                {
                    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 400, 400), "GAMEOVER\n" + " your score is " + userAction.getScore().ToString());
                    if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 45, 90, 90), "Restart"))
                    {
                        userAction.resetScore();
                        userAction.setLife();
                        //isOver = false;
                    }
                }

                else//游戏正常执行中，确定当前回合数
                {
                    GUI.color = Color.red;
                    GUI.Label(new Rect(3 * Screen.width / 4, 0, 400, 400), "Round: " + (userAction.getRound() + 1).ToString());
                    if (userAction.isInRound()==2 && GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 45, 90, 90), "Next Round"))//在回合间隔中，按钮进入下一回合
                    {
                        userAction.setRoundState(0);
                    }

                }
            }
            else if (userAction.getRound() == 5)
            {
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 400, 400), "YOU WIN!!!\n" + " your score is " + userAction.getScore().ToString());
                if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 45, 90, 90), "Restart"))
                {
                    userAction.resetScore();
                    userAction.setLife();
                    userAction.setRound(-1);
                    //isOver = false;
                }
            }
            else//游戏未开始
            {
                if(GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 45, 90, 90), "Start"))
                {
                    isOver = false;
                    userAction.setRoundState(0);
                }
            }
        }
    }
}
