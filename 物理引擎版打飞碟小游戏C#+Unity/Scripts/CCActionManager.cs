using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class CCActionManager : SSActionManager, ISSActionCallback, ActionManagerAdapter
{
    private FirstSceneControl sceneControl;
    private List<CCFlyAction> flys = new List<CCFlyAction>();
    private int diskNumber = 0;

    private List<SSAction> used = new List<SSAction>();
    private List<SSAction> free = new List<SSAction>();

    public void setDiskNumber(int dn)
    {
        diskNumber = dn;
    }

    public int getDiskNumber()
    {
        return diskNumber;
    }

    public SSAction getSSAction()
    {
        SSAction action = null;
        if (free.Count > 0)
        {
            action = free[0];
            free.Remove(free[0]);
        }
        else
        {
            action = ScriptableObject.Instantiate<CCFlyAction>(flys[0]);
        }
        used.Add(action);
        return action;
    }

    public void freeSSAction(SSAction action)
    {
        foreach (SSAction a in used)
        {
            if (a.GetInstanceID() == action.GetInstanceID())
            {
                a.reset();
                free.Add(a);
                used.Remove(a);
                break;
            }
        }
    }

    protected void Start()
    {
        sceneControl = (FirstSceneControl)Director.getInstance().current;
        sceneControl.actionManager = this;
        flys.Add(CCFlyAction.getCCFlyAction());
        base.flag = true;
    }

    private new void Update()
    {
        if (sceneControl.getGameState() == GameState.RUNNING)
            base.Update();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intPram = 0
        , string strParm = null, Object objParm = null)
    {
        if (source is CCFlyAction)
        {
            diskNumber--;
            Singleton<DiskFactory>.Instance.freeDisk(source.gameObject);
            freeSSAction(source);
        }
    }

    public void startThrow(Queue<GameObject> diskQueue)
    {
        foreach (GameObject i in diskQueue)
        {
            runAction(i, getSSAction(), (ISSActionCallback)this);
        }
    }
}