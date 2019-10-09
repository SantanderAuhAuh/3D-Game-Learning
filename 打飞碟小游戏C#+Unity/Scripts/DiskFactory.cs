using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
//using System;
using DiskClass;

namespace DiskFactoryClass
{
    public class DiskFactory : MonoBehaviour
    {
        public GameObject diskModel = null;                   /* 飞碟游戏对象 */
        private List<Disk> used = new List<Disk>();           /* 类似pool，用于减少内存消耗 */
        private List<Disk> free = new List<Disk>();

        public GameObject GetOneDisk(int roundNow)
        {
            diskModel = null;
            //System.Random randomNum = new System.Random(1);
            //Debug.Log("DiskFactory:" + free.Count +" "+ used.Count);
            if (free.Count > 0)
            {
                diskModel = free[0].gameObject;
                free.Remove(free[0]);
            }
            else
            {
                int id = Random.Range(-10, 10);
                if (id > roundNow/2)
                {
                    //Debug.Log("white");
                    diskModel = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Plate1"), Vector3.zero, Quaternion.identity);
                    diskModel.AddComponent<Disk>();                       /* 将GameObject加入Scene */
                    diskModel.GetComponent<Disk>().id = true;
                }
                else
                {
                    //Debug.Log("red");
                    diskModel = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Plate2"), Vector3.zero, Quaternion.identity);
                    diskModel.AddComponent<Disk>();
                    diskModel.GetComponent<Disk>().id = false;                
                    diskModel.GetComponent<Disk>().value = 1;
                }
                diskModel.GetComponent<Disk>().speed = Random.Range(0,5);
                float runX = UnityEngine.Random.Range(-1F, 1F) < 0 ? -1 : 1;
                //diskModel.GetComponent<Renderer>().material.color = diskModel.GetComponent<Disk>().color;            /* 设置Scene中GameObject的显示颜色，Renderer是显示类 */
                diskModel.GetComponent<Disk>().direction = new Vector3(runX, 1, 0);
            }
            //diskModel.GetComponent<Disk>().transform.Rotate(new Vector3(1, 0, 100));
            used.Add(diskModel.GetComponent<Disk>());
            diskModel.name = diskModel.GetInstanceID().ToString();
            return diskModel;
        }

        public void FreeUsingDisk(GameObject disk)
        {
            for (int i = 0; i < used.Count; i++)
            {
                if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
                {
                    used[i].gameObject.SetActive(false);
                    free.Add(used[i]);
                    used.Remove(used[i]);
                    break;
                }
            }
        }
    }
}
