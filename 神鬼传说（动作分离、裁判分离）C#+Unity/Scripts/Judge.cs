using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using SceneController;
using View;
using Director;

namespace Judge
{
    public interface ISSJudge
    {
        bool jude(Role[] roles);
    }

    public class SSJudge: MonoBehaviour, ISSJudge
    {
        pageImage gameGUI = (SSDirector.getInstance().currentSceneController as Controller).gameGUI;
        Role[] roles = (SSDirector.getInstance().currentSceneController as Controller).roles;
        Boat boat = (SSDirector.getInstance().currentSceneController as Controller).boat;

        public static SSJudge GetSSJudge()
        {
            SSJudge judger = new SSJudge();
            return judger;
        }

        public bool jude(Role[] roles)
        {
            int priestNumAtStart = 0;
            int priestNumAtEnd = 0;
            int devilNumAtStart = 0;
            int devilNumAtEnd = 0;
            int priestNumAtBoat = 0;
            int devilNumAtBoat = 0;
            for (int i = 0; i < 6; i++)
            {
                switch (roles[i].getRolePos())
                {
                    case 0:
                        if (roles[i].getRoleId())
                        {
                            priestNumAtBoat++;
                        }
                        else
                        {
                            devilNumAtBoat++;
                        }
                        break;
                    case 1:
                        if (roles[i].getRoleId())
                        {
                            priestNumAtStart++;
                        }
                        else
                        {
                            devilNumAtStart++;
                        }
                        break;
                    case 2:
                        if (roles[i].getRoleId())
                        {
                            priestNumAtEnd++;
                        }
                        else
                        {
                            devilNumAtEnd++;
                        }
                        break;
                    default:
                        break;
                }
            }
            Debug.Log("priestNumAtBoat:" + priestNumAtBoat);
            Debug.Log("priestNumAtStart:" + priestNumAtStart);
            Debug.Log("devilNumAtBoat:" + devilNumAtBoat);
            Debug.Log("devilNumAtStart:" + devilNumAtStart);
            Debug.Log("priestNumAtEnd:" + priestNumAtEnd);
            Debug.Log("devilNumAtEnd:" + devilNumAtEnd);
            Debug.Log("");
            
            if (((boat.getBoatPos() ? priestNumAtBoat : 0) + priestNumAtStart < (boat.getBoatPos()?devilNumAtBoat:0) + devilNumAtStart)&&(priestNumAtStart!=0) || ((boat.getBoatPos() ? 0 : priestNumAtBoat) + priestNumAtEnd < (boat.getBoatPos() ? 0:devilNumAtBoat) + devilNumAtEnd)&&(priestNumAtEnd!=0))
            {
                return false;
            }
            else
                return true;
        }

        protected void Update()
        {
            gameGUI.isOver = jude(roles);
        }
    }

}
