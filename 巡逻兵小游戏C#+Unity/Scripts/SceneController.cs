using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour, IUserAction, ISceneController
{

    // 当前玩家处于哪个区域
    public int areaSign = -1;
    // 巡逻兵工厂
    public PatrolFactory patrolFactory;
    // 物品工厂
    public StuffFactory stuffFactory;
    // 动作管理器
    public PatrolActionManager actionManager;
    // 主相机
    public Camera mainCamera;
    // 玩家
    public GameObject player;
    // 记分员
    public ScoreController scoreController;
    // 巡逻兵列表
    private List<GameObject> patrols;
    // 物品列表
    private List<GameObject> stuffs;
    private float playerSpeed = 5f;
    private float rotateSpeed = 135f;
    private bool isGameOver = false;

    // Use this for initialization
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        patrolFactory = Singleton<PatrolFactory>.Instance;
        stuffFactory = Singleton<StuffFactory>.Instance;
        actionManager = gameObject.AddComponent<PatrolActionManager>() as PatrolActionManager;
        LoadResources();
        mainCamera.GetComponent<CameraMan>().target = player;
        scoreController = Singleton<ScoreController>.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < patrols.Count; i++)
        {
            patrols[i].GetComponent<PatrolData>().areaSign = areaSign;
        }
        if (scoreController.stuffTotal == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        patrolFactory.stopPatrol();
        actionManager.DestroyAll();
    }

    public int GetCrystalNum()
    {
        return scoreController.stuffTotal;
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }

    public int GetScore()
    {
        return scoreController.score;
    }

    public void MovePlayer(int dir)
    {
        if (!isGameOver)
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, dir * 90, 0));
            player.GetComponent<Animator>().SetBool("isRun", true);
            switch (dir)
            {
                case Diretion.UP:
                    player.transform.position += new Vector3(0, 0, 0.1f);
                    break;
                case Diretion.DOWN:
                    player.transform.position += new Vector3(0, 0, -0.1f);
                    break;
                case Diretion.LEFT:
                    player.transform.position += new Vector3(-0.1f, 0, 0);
                    break;
                case Diretion.RIGHT:
                    player.transform.position += new Vector3(0.1f, 0, 0);
                    break;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("SimplePatrols");
    }

    public void LoadResources()
    {
        player = Instantiate(Resources.Load("Prafabs/Player"), new Vector3(2, 0, 2), Quaternion.identity) as GameObject;
        // player.AddComponent<Rigidbody>();
        player.tag = "Player";
        stuffs = stuffFactory.getCrystals();
        patrols = patrolFactory.getPatrols();
        for (int i = 0; i < patrols.Count; i++)
        {
            actionManager.GoPatrol(patrols[i]);
        }
    }

    void OnEnable()
    {
        Debug.Log("Scene controller onEnable");
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameOverChange += GameOver;
        GameEventManager.StuffChange += ReduceCrystalNumber;
    }

    private void ReduceCrystalNumber()
    {
        scoreController.stuffTotal--;
    }

    private void AddScore()
    {
        scoreController.score++;
    }

    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameOverChange -= GameOver;
        GameEventManager.StuffChange -= ReduceCrystalNumber;
    }
}