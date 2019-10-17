using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab;
    public List<DiskData> used = new List<DiskData>();
    public List<DiskData> free = new List<DiskData>();

    private Director director = Director.getInstance();

    private void Awake()
    {
        diskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk"), Vector3.zero, Quaternion.identity);
        diskPrefab.SetActive(false);
    }

    public GameObject getDisk(int round,bool isRigidbody)
    {
        GameObject disk = null;
        if (free.Count > 0)
        {
            disk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            disk = GameObject.Instantiate<GameObject>(diskPrefab, Vector3.zero, Quaternion.identity);
            disk.AddComponent<DiskData>();
        }

        if (isRigidbody && disk.GetComponent<Rigidbody>() == null)        /* 添加刚体性质 */
        {
            disk.AddComponent<Rigidbody>();
        }

        int start;
        switch (round)
        {
            case 0: start = 0; break;
            case 1: start = 100; break;
            default: start = 200; break;
        }
        int selectColor = Random.Range(start, round * 499);
        round = selectColor / 250;
        DiskData diskData = disk.GetComponent<DiskData>();
        Renderer renderer = disk.GetComponent<Renderer>();
        Renderer childRenderer = disk.transform.GetChild(0).GetComponent<Renderer>();
        float ranX = Random.Range(-1, 1) < 0 ? -1.2f : 1.2f;
        Vector3 direction = new Vector3(ranX, 1, 0);
        switch (round)
        {
            case 0:
                diskData.setDiskData(new Vector3(1.35f, 1.35f, 1.35f), Color.white, 4.0f, direction);
                renderer.material.color = Color.white;
                childRenderer.material.color = Color.white;
                break;
            case 1:
                diskData.setDiskData(new Vector3(1f, 1f, 1f), Color.gray, 6.0f, direction);
                renderer.material.color = Color.yellow;
                childRenderer.material.color = Color.yellow;
                break;
            case 2:
                diskData.setDiskData(new Vector3(0.7f, 0.7f, 0.7f), Color.black, 8.0f, direction);
                renderer.material.color = Color.blue;
                childRenderer.material.color = Color.blue;
                break;
        }
        used.Add(diskData);
        diskData.name = diskData.GetInstanceID().ToString();
        disk.transform.localScale = diskData.getSize();

        return disk;
    }

    public void freeDisk(GameObject disk)
    {
        DiskData temp = null;
        foreach (DiskData i in used)
        {
            if (disk.GetInstanceID() == i.gameObject.GetInstanceID())
            {
                temp = i;
            }
        }
        if (temp != null)
        {
            temp.gameObject.SetActive(false);
            free.Add(temp);
            used.Remove(temp);
        }
    }
}