using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

namespace MyGame
{
    public class FirstSceneControl : MonoBehaviour, ISceneControl, IUserAction
    {
        public ActionManagerAdapter actionManager { set; get; }
        public ScoreRecorder scoreRecorder { set; get; }
        public Queue<GameObject> diskQueue = new Queue<GameObject>();
        private int diskNumber = 0;
        private int currentRound = -1;
        private float time = 0;
        private GameState gameState = GameState.START;

        private bool isPhysical = false;                /* 决定是否进行物理运动 */

        void Awake()
        {
            Director director = Director.getInstance();
            director.current = this;
            diskNumber = 10;
            this.gameObject.AddComponent<ScoreRecorder>();
            this.gameObject.AddComponent<DiskFactory>();
            //this.gameObject.AddComponent<ActionManagerAdapter>();
            scoreRecorder = Singleton<ScoreRecorder>.Instance;
            //actionManager = new CCActionManager();
            director.current.loadResources();
        }

        public void loadResources()
        {

        }

        private void Update()
        {
            if (actionManager == null)                        /* 必须先设置运动模式，若不点击切换，则默认运动学模式 */
            {
                return;
            }

            if (actionManager.getDiskNumber() == 0 && gameState == GameState.RUNNING)
            {
                gameState = GameState.ROUND_FINISH;

                /*if (isPhysical)
                {
                    gameState = GameState.FUNISH;
                    return;
                }*/
                if (currentRound == 2)
                {
                    gameState = GameState.FUNISH;
                    return;
                }
            }
            if (actionManager.getDiskNumber() == 0 && gameState == GameState.ROUND_START)
            {
                currentRound++;
                nextRound();
                actionManager.setDiskNumber(10);
                gameState = GameState.RUNNING;
            }
            if (time > 1 && gameState != GameState.PAUSE)
            {
                throwDisk();
                time = 0;
            }
            else
            {
                time += Time.deltaTime;
            }
        }

        private void nextRound()
        {
            DiskFactory diskFactory = Singleton<DiskFactory>.Instance;
            for (int i = 0; i < diskNumber; i++)
            {
                diskQueue.Enqueue(diskFactory.getDisk(currentRound,isPhysical));
            }
            actionManager.startThrow(diskQueue);
        }

        void throwDisk()
        {
            if (diskQueue.Count != 0)
            {
                GameObject disk = diskQueue.Dequeue();
                Vector3 pos = new Vector3(-disk.GetComponent<DiskData>().getDirection().x * 10, Random.Range(0f, 4f), 0);
                disk.transform.position = pos;
                disk.SetActive(true);
            }
        }

        public int getScore()
        {
            return scoreRecorder.score;
        }

        public GameState getGameState()
        {
            return gameState;
        }

        public void setGameState(GameState gameState)
        {
            this.gameState = gameState;
        }

        public bool getActionMode()
        {
            return isPhysical;
        }

        public void setActionMode(bool mode)
        {
            this.isPhysical = mode;
            if (isPhysical)
            {
                this.gameObject.AddComponent<ModifiedActionManager>();
            }
            else
            {
                this.gameObject.AddComponent<CCActionManager>();
            }
        }

        public void hit(Vector3 pos)
        {
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(pos));
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider.gameObject.GetComponent<DiskData>() != null)
                {
                    scoreRecorder.record(hit.collider.gameObject);
                    hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
                }
            }
        }
    }
}
