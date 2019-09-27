using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Director;
using UserActions;
using Models;
using View;
using SceneController;
using ActionManager;
using Judge;

/* 场记类实现：需要继承场记接口 */
public class Controller : MonoBehaviour, ISceneController, IUserAction
{
    public Land startLand, endLand;
    public Boat boat;
    public Role[] roles;
    public pageImage gameGUI;

    public SSActionManager actionManager;//新增
    public SSJudge judger;

    /* 场记接口方法的实现，主要完成场景载入 */
    public void LoadResource()
    {
        startLand = new Land(true);
        endLand = new Land(false);
        boat = new Boat();
        roles = new Role[6];
        for (int i = 0; i < 3; i++)
        {
            roles[i] = new Role(true, "Priest" + i);
        }
        for (int i = 3; i < 6; i++)
        {
            roles[i] = new Role(false, "Devil" + i);
        }
    }
    /* 用户行为接口方法的实现 */
    public void MoveBoat()
    {
        if (boat.isEmpty() || boat.isMove()|| !gameGUI.isOver) return;
        CCMoveToAction moveBoat = CCMoveToAction.GetCCMoveToAction(boat.moveToOtherSide());//新增
        actionManager.RunAction(boat.GetGameObject(), moveBoat);
        //gameGUI.isOver = Check();
    }
    public void MoveRole(Role role)
    {
        if (!boat.isMove() && gameGUI.isOver)
        {
            if (role.getRolePos() == 0)
            {
                CCMoveToAction moveRoleToAshore = CCMoveToAction.GetCCMoveToAction(role.goAshore());
                actionManager.RunAction(role.GetGameObject(), moveRoleToAshore);
            }
            else
            {
                CCMoveToAction moveRoleToBoat = CCMoveToAction.GetCCMoveToAction(role.goBoarding());
                actionManager.RunAction(role.GetGameObject(), moveRoleToBoat);
            }
        }
    }
    /*public bool Check()
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
        if ((priestNumAtBoat + priestNumAtStart < devilNumAtBoat + devilNumAtStart || priestNumAtBoat + priestNumAtEnd < devilNumAtBoat + devilNumAtEnd))
        {
            return false;
        }
        else
            return true;
    }*/
    public void Resume()
    {
        startLand.reset();
        endLand.reset();
        boat.reset();
        for (int i = 0; i < roles.Length; i++)
        {
            roles[i].reset();
        }
    }

    /* 控制器的生命周期 */
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResource();
    }

    void Start()
    {
        Debug.Log("First SceneController Start!");
        gameGUI = gameObject.AddComponent<pageImage>() as pageImage;
        actionManager = gameObject.AddComponent<SSActionManager>() as SSActionManager;
        judger = gameObject.AddComponent<SSJudge>() as SSJudge;
    }
}
