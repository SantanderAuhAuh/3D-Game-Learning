using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float value;

    private float temp;

    void Start()
    {
        value = 0.0f;
        temp = 0.0f;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(250, 20, 40, 20), "+"))
        {
            if(temp<10)
                temp++;
        }

        if (GUI.Button(new Rect(300, 20, 40, 20), "-"))
        {
            if(temp>0)
                temp--;
        }

        value = Mathf.Lerp(value, temp, 0.01f);

        GUI.color = Color.red;
        GUI.HorizontalScrollbar(new Rect(20, 20, 200, 20), 0.0f, value, 0.0f, 10.0f);
    }
}
