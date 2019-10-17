using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public enum SSActionEventType : int { Started, Completed }

    public interface ISSActionCallback
    {
        void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intPram = 0
            , string strParm = null, Object objParm = null);
    }
}