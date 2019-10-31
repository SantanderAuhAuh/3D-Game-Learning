using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{

    // 发布分数变化的事件给订阅者
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    // 发布游戏结束的事件给订阅者
    public delegate void GameOverEvent();
    public static event GameOverEvent GameOverChange;
    // 发布物品减少的事件给订阅者
    public delegate void StuffEvent();
    public static event StuffEvent StuffChange;

    public void playerEscape()
    {
        if (ScoreChange != null)
        {
            Debug.Log("Add score!");
            ScoreChange();
        }
    }

    public void gameOver()
    {
        if (GameOverChange != null)
        {
            GameOverChange();
        }
    }

    public void ReduceStuffNum()
    {
        if (StuffChange != null)
        {
            StuffChange();
        }
    }
}