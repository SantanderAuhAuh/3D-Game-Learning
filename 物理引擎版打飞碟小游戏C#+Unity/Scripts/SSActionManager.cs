using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();
    protected bool flag = true;                                /* 判别使用运动学还是物理运动 */

    protected void Update()
    {
        foreach (SSAction action in waitingAdd)
        {
            actions[action.GetInstanceID()] = action;
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> i in actions)
        {
            SSAction value = i.Value;
            if (value.destroy)
            {
                waitingDelete.Add(value.GetInstanceID());
            }
            else if (value.enable && flag)
            {
                value.Update();
            }
            else if(value.enable && !flag)
            {
                value.FixedUpdate();
            }
        }

        foreach (int i in waitingDelete)
        {
            SSAction ac = actions[i];
            actions.Remove(i);
            Destroy(ac);                                /* 版本更新，DestroyObject函数已被弃用 */
        }
    }

    public void runAction(GameObject gameObject, SSAction action, ISSActionCallback manager)
    {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void stopRigidbodyAction()
    {
        foreach (SSAction action in actions.Values)
        {
            action.rigidbodyStopAction();
        }
    }

    public void startRigidbodyAction()
    {
        foreach (SSAction action in actions.Values)
        {
            action.rigidbodyStartAction();
        }
    }
}