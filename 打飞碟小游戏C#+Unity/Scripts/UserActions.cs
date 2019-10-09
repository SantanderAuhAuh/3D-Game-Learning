using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserActions
{
    public interface IUserAction
    {
        int isInRound();
        void setRoundState(int state);
        int getScore();
        void click(Vector3 pos);
        int getRound();
        void setRound(int num);
        void resetScore();
        int getLife();
        void setLife();
    }

}
