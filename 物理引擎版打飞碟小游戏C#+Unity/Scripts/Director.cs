using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : System.Object
{
    public ISceneControl current { set; get; }

    private static Director _Instance;

    public static Director getInstance()
    {
        return _Instance ?? (_Instance = new Director());
    }
}