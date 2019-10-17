using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { ROUND_START, ROUND_FINISH, RUNNING, PAUSE, START, FUNISH }

public interface IUserAction
{
    GameState getGameState();
    void setGameState(GameState gameState);
    int getScore();
    void hit(Vector3 pos);
    bool getActionMode();
    void setActionMode(bool mode);
}