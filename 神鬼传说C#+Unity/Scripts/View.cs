using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserActions;
using Director;

namespace View
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
            if (!isOver)
            {
                GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 80, 100, 50), "Gameover!", new GUIStyle(){fontSize = 30});
                if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2, 100, 50), "Restart"))
                {
                    userAction.Resume();
                    isOver = true;
                }
            }
            if(GUI.Button(new Rect(Screen.width / 2- 60, Screen.height / 2-150, 100, 50), "Go"))
            {
                userAction.MoveBoat();
            }
        }
    }
}
