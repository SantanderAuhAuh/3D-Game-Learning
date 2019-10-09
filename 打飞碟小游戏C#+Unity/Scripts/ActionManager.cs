using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Director;
using SingletonClass;
using DiskClass;
using DiskFactoryClass;

namespace ActionManager
{
    public enum SSActionEventType : int { Started, Competeted }

    public interface ISSActionCallback/* 动作事件调用接口，由动作、动作类型和参数构成事件 */
    {
        void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
            int intParam = 0, string strParam = null, Object objectParam = null);
    }

    public class SSAction : ScriptableObject            /* 动作 */
    {

        public bool enable = true;                      /* 是否正在进行此动作 */
        public bool destroy = false;                    /* 是否需要被销毁 */

        public GameObject gameobject;                   /* 动作对象 */
        public Transform transform;                     /* 动作对象的transform */
        public ISSActionCallback callback;              /* 回调函数 */

        protected SSAction() { }                        /* 保证SSAction不会被new */

        public virtual void Start()                     /* 子类可以使用这两个函数 */
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update()
        {
            throw new System.NotImplementedException();
        }
    }

    public class CCSequenceAction : SSAction, ISSActionCallback/* 组合动作类 */
    {
        List<SSAction> sequence;                        /* 动作列表 */
        int repeat;                                     /* 标识动作是否进入循环 */
        int start;                                      /* 动作列表中需要执行的下一个动作 */

        public static CCSequenceAction GetCCSequenceAction(List<SSAction> actions, int repeat, int start)
        {                                               /* 连续动作类的实例化 */
            CCSequenceAction SeqAction = ScriptableObject.CreateInstance<CCSequenceAction>();
            SeqAction.sequence = actions;
            SeqAction.repeat = repeat;
            SeqAction.start = start;
            return SeqAction;
        }

        public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
            int intParam = 0, string strParam = null, Object objectParam = null)
        {
            source.destroy = false;
            this.start++;
            if (this.start >= sequence.Count)
            {
                this.start = 0;
                if (repeat > 0) repeat--;              /* 组合动作循环执行的次数 */
                else
                {
                    this.destroy = true;               /* 所有动作执行完毕，组合动作等待销毁 */
                    this.callback.SSActionEvent(this); /* 向上调用动作管理类的ISSActionCallback类方法，进行结束操作 */
                }
            }
        }

        public override void Start()
        {
            //base.Start();
            for (int i = 0; i < sequence.Count; i++)
            {
                sequence[i].gameobject = this.gameobject;/* 所有组合动作的子动作拥有同一个作用对象 */
                sequence[i].callback = this;             /* 所有组合动作的子动作需要在结束时通知组合动作对象 */
                sequence[i].transform = this.transform;
                sequence[i].Start();                     /* 子动作进行 */
            }
        }

        public override void Update()
        {
            //base.Update();
            if (start < sequence.Count)                  /* start值只有子动作结束时才会改变，所以这里的Update是子动作的 */
            {
                sequence[start].Update();
            }
        }
    }

    public class CCFlyAction : SSAction
    {
        float g;                                 /* 重力常量 */
        float time;

        public override void Start()
        {
            enable = true;
            g = 9.8F;
            time = 0;
        }
        
        public override void Update()
        {
            if (gameobject.activeSelf)
            {
                time += Time.deltaTime;
                transform.Translate(Vector3.down * g * time * Time.deltaTime);//添加向下的速度向量
                transform.Translate(gameobject.GetComponent<Disk>().direction * gameobject.GetComponent<Disk>().speed * Time.deltaTime); 
                if (this.transform.position.y < -4)
                {
                    this.destroy = true;
                    this.enable = false;
                    this.callback.SSActionEvent(this);
                }
            }
        }
    }

    public class SSActionManager : MonoBehaviour, ISSActionCallback
    {
        List<SSAction> actionList=new List<SSAction>();       /* 必须初始化！！！！ */

        public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
            int intParam = 0, string strParam = null, Object objectParam = null)
        {
            source.enable = false;                            /* 动作完成执行 */
        }

        public void RunAction(GameObject gameObject, SSAction action)
        {
            action.gameobject = gameObject;                   /* 为需要执行的动作初始化 */
            action.transform = gameObject.transform;
            action.callback = this;                           /* 提交给动作管理类的动作自然由此类管理 */
            actionList.Add(action);                           /* 加入动作列表，等待循环遍历，有的动作可能反复执行 */
            action.Start();                                   /* 主要针对组合动作，需要对子动作都进行初始化 */
        }

        protected void Update()
        {
            for (int i = 0; i < actionList.Count; i++)
            {
                if (actionList[i].destroy)
                    actionList.Remove(actionList[i]);
                else if (actionList[i].enable)
                    actionList[i].Update();
            }
        }
    }

    public class CCActionManager : SSActionManager, ISSActionCallback
    {
        public Controller sceneController;
        public int disk_number = 0;


        void Start()
        {
            sceneController = SSDirector.getInstance().currentSceneController as Controller;
            sceneController.actionManager = this;
        }

        public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
            int intParam = 0, string strParam = null, Object objectParam = null)
        {
            if (source is CCFlyAction)
            {
                disk_number--;
                DiskFactory df = Singleton<DiskFactory>.Instance;
                df.FreeUsingDisk(source.gameobject);
            }
        }


        public void loadAction(GameObject disk)
        {
                RunAction(disk, ScriptableObject.CreateInstance<CCFlyAction>());
            
        }
    }

}
