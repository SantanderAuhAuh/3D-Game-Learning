using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserActions;
using Director;
using DiskClass;
using GameView;
using DiskFactoryClass;
using ActionManager;
using SceneController;
using SingletonClass;

/* 场记类实现：需要继承场记接口 */
public class Controller : MonoBehaviour, ISceneController, IUserAction
{
    public pageImage gameGUI;
    public CCActionManager actionManager;

    public Queue<GameObject> disks = new Queue<GameObject>();
    public int score = 0;
    private int diskNumber=5;
    private int currentRound = -1;
    public int life = 5;
    public int finalRound = 5;
    public int inRound = 0;

    private float time = 0;     //计时
    private float roundTime = 1;    //每个回合隔多久飞一次飞碟
    /* 场记接口方法的实现，主要完成场景载入 */
    public void LoadResource()
    {
        //无需载入初始场景
    }

    public int isInRound()
    {
        return inRound;
    }
    public void setRoundState(int state)
    {
        inRound = state;
    }
    public int getScore()
    {
        return score;
    }
    public void click(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] clicks;
        clicks = Physics.RaycastAll(ray);

        for (int i = 0; i < clicks.Length; i++)
        {
            RaycastHit hit = clicks[i];
            if (hit.collider.gameObject.GetComponent<Disk>() != null)
            {
                if (hit.collider.gameObject.GetComponent<Disk>().id)
                {
                    score += hit.collider.gameObject.GetComponent<Disk>().value;
                    hit.collider.gameObject.transform.position = new Vector3(0, -10, 0);
                }
                else
                {
                    life--;
                    hit.collider.gameObject.transform.position = new Vector3(0, -10, 0);
                }
            }
        }
    }
    public int getRound()
    {
        return currentRound;
    }
    public void setRound(int num)
    {
        currentRound = num;
    }
    public void resetScore()
    {
        score = 0;
    }
    public int getLife()
    {
        return life;
    }
    public void setLife()
    {
        life = 5;
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
        actionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        this.gameObject.AddComponent<DiskFactory>();
    }

    private void Update()
    {
        if (inRound==2 && currentRound==finalRound)
        {
            gameGUI.isOver = true;
        }
        else
        {
            
            if (actionManager.disk_number == 0 && inRound==1&&!gameGUI.isOver)
            {
                inRound = 2;
            }

            if (actionManager.disk_number == 0 && inRound==0&&!gameGUI.isOver)
            {
                currentRound++;
                actionManager.disk_number = (currentRound + 1) * diskNumber;
                Debug.Log("Controller's disk number:" + actionManager.disk_number);
                inRound = 1;
            }
            Debug.Log(actionManager.disk_number);
            if (time > roundTime)
            {
                if (actionManager.disk_number>0)
                { 
                    DiskFactory df = Singleton<DiskFactory>.Instance;
                    time = 0;
                    actionManager.loadAction(df.GetOneDisk(currentRound));
                }
            }
            else
            {
                time += Time.deltaTime;
            }
        }

    }

    
}
