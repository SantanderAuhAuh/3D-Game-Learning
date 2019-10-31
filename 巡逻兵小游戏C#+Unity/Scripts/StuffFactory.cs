using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffFactory : MonoBehaviour
{

    private GameObject stuff = null;
    private List<GameObject> usedStuff = new List<GameObject>();
    private float range = 10;       // 生成的坐标范围

    private static StuffFactory instance = null;

    public List<GameObject> getCrystals()
    {
        for (int i = 0; i < 12; i++)
        {
            stuff = Instantiate(Resources.Load<GameObject>("Prafabs/Ball"));
            float ranx = UnityEngine.Random.Range(-range, range);
            float ranz = UnityEngine.Random.Range(-range, range);
            stuff.transform.position = new Vector3(ranx, 0, ranz);
            usedStuff.Add(stuff);
        }
        return usedStuff;
    }

    public static StuffFactory getInstance()
    {
        if (instance == null) instance = new StuffFactory();
        return instance;
    }
}