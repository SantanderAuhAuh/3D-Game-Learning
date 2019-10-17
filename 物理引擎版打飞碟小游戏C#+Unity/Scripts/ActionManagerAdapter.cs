using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public interface ActionManagerAdapter
    {
        void setDiskNumber(int dn);

        int getDiskNumber();

        SSAction getSSAction();

        void freeSSAction(SSAction action);

        void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intPram = 0
        , string strParm = null, Object objParm = null);

        void startThrow(Queue<GameObject> diskQueue);
    }
}
