using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Director;
using SceneController;
using UserActions;

namespace Models
{
    public class Land                                     /* 陆地类 */
    {
        GameObject land;                                  /* 声明陆地对象 */
        Vector3[] rolesPositions;                         /* 保存陆地上人物的位置 */
        bool isStart;                                     /* 标记左右岸，默认右岸为Start */
        Vector3 landPosition;                             /* 岸的位置 */
        bool[] isPosEmpty = new bool[6];                  /* 保存岸上位置的占用的情况 */

        public Land(bool startOrEnd)                      /* true为右岸，false为左岸 */
        {
            if (startOrEnd)
            {
                landPosition = new Vector3(3, -1, -2);
                rolesPositions = new Vector3[] {new Vector3(1,1,-4), new Vector3(1,1,-2), new Vector3(2,1,-4),
                new Vector3(2,1,-2),new Vector3(3,1,-4),new Vector3(3,1,-2)};
            }
            else
            {
                landPosition = new Vector3(-20, -1, -2);
                rolesPositions = new Vector3[] {new Vector3(-18,1,-4), new Vector3(-18,1,-2), new Vector3(-19,1,-4),
                new Vector3(-19,1,-2),new Vector3(-20,1,-4),new Vector3(-20,1,-2)};
            }
            land = Object.Instantiate(Resources.Load("Land", typeof(GameObject)), landPosition, Quaternion.identity) as GameObject;
            isStart = startOrEnd;
            for (int i = 0; i < 6; i++) isPosEmpty[i] = true;
        }

        public Vector3 getPos(int index)                   /* 获得一个岸上的位置 */
        {
            return rolesPositions[index];
        }

        public int getAnEmptyIndex()
        {
            for(int i = 0; i < isPosEmpty.Length; i++)
            {
                if (isPosEmpty[i])
                    return i;
            }
            return -1;
        }

        public bool isEmpty()                              /* 岸上是否为空 */
        {
            for(int i = 0; i < isPosEmpty.Length; i++)
            {
                if (!isPosEmpty[i])
                    return false;
            }
            return true;
        }

        public bool judgeId()                              /* 判断是左岸还是右岸 */
        {
            return isStart;
        }

        public void setOccupied(int index)                /* 将位置设为占用 */
        {
            isPosEmpty[index] = false;
        }

        public void setEmpty(int index)                  /* 将位置设置为空闲 */
        {
            isPosEmpty[index] = true;
        }

        public int getSeatByPos(Vector3 pos)            /* 根据坐标获得人物在岸上的位置 */
        {
            for(int i = 0; i < rolesPositions.Length; i++)
            {
                if (rolesPositions[i] == pos)
                    return i;
            }
            return -1;
        }

        public void reset()                               /* 所有位置置空 */
        {
            for (int i = 0; i < isPosEmpty.Length; i++)
            {
                isPosEmpty[i] = true;
            }
        }
    }

    public class Boat                                     /* 小船类 */
    {
        GameObject boat;
        Move move;
        Click click;
        Vector3[] rolePositionsAtStart;                   /* 小船在右岸时的座位位置 */
        Vector3[] rolePositionsAtEnd;                     /* 小船在左岸时的座位位置 */
        bool[] isPosEmpty = new bool[2];                  /* 座位是否空闲 */
        bool boatPosition;                                /* 小船的位置,右岸为true，左岸为false */
        Vector3 startPos, endPos;                         /* 小船在起点和终点时的位置 */
        
        public Boat()
        {
            /* 起点和终点的船上位置坐标必须对应，调试了好久！！ */
            startPos = new Vector3(-1, -1, -1);
            rolePositionsAtStart = new Vector3[] { new Vector3(-0.5F, 0.5F, -1), new Vector3(-1.6F, 0.5F, -1) };
            endPos = new Vector3(-16, -1, -1);
            rolePositionsAtEnd = new Vector3[] { new Vector3(-15.5F, 0.5F, -1), new Vector3(-16.6F, 0.5F, -1) };
            boatPosition = true;
            boat= Object.Instantiate(Resources.Load("Boat", typeof(GameObject)), startPos, Quaternion.identity) as GameObject;
            move = boat.AddComponent(typeof(Move)) as Move;
            click = boat.AddComponent(typeof(Click)) as Click;
            move.moveTowards(startPos);
            for (int i = 0; i < isPosEmpty.Length; i++) isPosEmpty[i] = true;
        }

        public Vector3 getPos(int index)                   /* 获得一个岸上的位置 */
        {
            if (boatPosition)
                return rolePositionsAtStart[index];
            else
                return rolePositionsAtEnd[index];
        }

        public int getAnEmptyIndex()
        {
            for (int i = 0; i < isPosEmpty.Length; i++)
            {
                if (isPosEmpty[i])
                    return i;
            }
            return -1;
        }

        public int getSeatByPos(Vector3 pos)               /* 根据坐标获得人物在船上的位置 */
        {
            if (boatPosition)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (rolePositionsAtStart[i] == pos)
                        return i;
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (rolePositionsAtEnd[i] == pos)
                        return i;
                }
            }
            return -1;
        }
            
        public void setOccupied(int index)                /* 将位置设为占用 */
        {
            isPosEmpty[index] = false;
        }

        public void setEmpty(int index)                  /* 将位置设置为空闲 */
        {
            isPosEmpty[index] = true;
        }

        public bool isEmpty()                              /* 船上是否为空 */
        {   
            for (int i = 0; i < isPosEmpty.Length; i++)
            {
                if (!isPosEmpty[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool isMove()
        {
            if (boat.transform.position != startPos && boat.transform.position != endPos)
                return true;
            else
                return false;
        }

        public void moveToOtherSide()                      /* 船向对岸移动 */
        {
            if (getBoatPos())
            {
                move.moveTowards(endPos);
                boatPosition = false;
            }
            else
            {
                move.moveTowards(startPos);
                boatPosition = true;
            }
        }

        public bool getBoatPos()                          /* 返回船在左岸还是右岸，右岸为true */
        {
            return boatPosition;
        }

        public GameObject getPrototype()                 /* 获得游戏对象 */
        {
            return boat;
        }

        public void reset()                               /* 所有位置置空 */
        {
            for(int i = 0; i < isPosEmpty.Length; i++)
            {
                isPosEmpty[i] = true;
            }
            boat.transform.position = startPos;
            boatPosition = true;
        }
    }

    public class Role                                     /* 人物类 */
    {
        GameObject role;
        Move move;                                        /* 移动 */
        Click click;
        bool id;                                          /* 判断身份，牧师或者恶魔，true为牧师，false为恶魔 */
        int isOnLand;                                     /* 判断是否在岸上，在船上是0，右岸为1，左岸为2 */
        Land startLand = (SSDirector.getInstance().currentSceneController as Controller).startLand;
        Land endLand = (SSDirector.getInstance().currentSceneController as Controller).endLand;
        Boat boat = (SSDirector.getInstance().currentSceneController as Controller).boat;

        public Role(bool hisId,string name)               /* 用name来标识人物 */
        {
            int emptyIndex = startLand.getAnEmptyIndex();
            if (emptyIndex == -1)
                Application.Quit();

            if (hisId)
            {
                role = Object.Instantiate(Resources.Load("Priest", typeof(GameObject)), startLand.getPos(emptyIndex), Quaternion.identity) as GameObject;
                startLand.setOccupied(emptyIndex);
            }
            else
            {
                role = Object.Instantiate(Resources.Load("Devil", typeof(GameObject)), startLand.getPos(emptyIndex), Quaternion.identity) as GameObject;
                startLand.setOccupied(emptyIndex);
            }
            role.name = name;
            isOnLand = 1;
            id = hisId;
            move = role.AddComponent(typeof(Move)) as Move;
            click = role.AddComponent(typeof(Click)) as Click;
            move.moveTowards(startLand.getPos(emptyIndex));
            click.SetRole(this);
        }

        public void setRolePos(Vector3 pos)                /* 设置人物位置 */
        {
            role.transform.position = pos;
        }

        public void goAshore()                             /* 人物上岸 */
        {
            Land land = boat.getBoatPos() ? startLand : endLand;
            int pos = land.getAnEmptyIndex();
            if (pos == -1)
            {
                Debug.Log("Land No Empty.");
                return;
            }
            land.setOccupied(pos);
            boat.setEmpty(boat.getSeatByPos(role.transform.position));

            //Debug.Log("Boat empty:" + boat.getSeatByPos(role.transform.position) + role.transform.position);

            move.moveTowards(land.getPos(pos));
            if (boat.getBoatPos()) isOnLand = 1;
            else isOnLand = 2;
            role.transform.parent = null;
        }

        public void goBoarding()                           /* 人物上船 */
        {
            Land land = boat.getBoatPos() ? startLand : endLand;
            int pos = boat.getAnEmptyIndex();
            if (pos == -1)
            {
                Debug.Log("Boat No Empty.");
                return;
            }
            boat.setOccupied(pos);
            land.setEmpty(land.getSeatByPos(role.transform.position));
            move.moveTowards(boat.getPos(pos));
            isOnLand = 0;
            role.transform.parent = boat.getPrototype().transform;
        }

        public int getRolePos()                           /* 了解人物在左岸（2）、右岸（1）还是船上（0） */
        {
            return isOnLand;
        }

        public bool getRoleId()                           /* 获得人物身份 */
        {
            return id;
        }

        public void reset()
        {
            int emptyIndex = startLand.getAnEmptyIndex();
            setRolePos(startLand.getPos(emptyIndex));
            startLand.setOccupied(emptyIndex);
            role.transform.parent = null;
            isOnLand = 1;
        }

    }

    public class Move: MonoBehaviour
    {
        int speed = 50;
        Vector3 destPos;
        bool canMove = false;
        private void Update()
        {
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, destPos, speed * Time.deltaTime);
                if(transform.position==destPos)
                    canMove = false;
            }
        }

        public void moveTowards(Vector3 dest)
        {
            destPos = dest;
            canMove = true;
        }
    }

    public class Click : MonoBehaviour
    {
        IUserAction userAction;
        Role role;

        public void SetRole(Role role)
        {
            this.role = role;
        }

        public void Start()
        {
            userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        }

        private void OnMouseDown()
        {
            if (role == null) return;
            else
            {
                userAction.MoveRole(role);
            }
        }
    }
}
