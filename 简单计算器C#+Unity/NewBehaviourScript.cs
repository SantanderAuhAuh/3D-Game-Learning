using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private double result = 0;
    private string current = "0";
    private string latestOp = "#";
    private bool latestCh;//最近输入的是否是运算符 
    private GUIStyle buttonStyle = new GUIStyle();
    private GUIStyle resultStyle = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Start!");
        result = 0;
        current = "0";
        latestOp = "#";
        resultStyle.alignment = TextAnchor.MiddleLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void calculate()
    {
        if (latestOp == "+")
        {
            result += double.Parse(current);
            current = result.ToString();
        }
        else if (latestOp == "-")
        {
            result -= double.Parse(current);
            current = result.ToString();
        }
        else if (latestOp == "*")
        {
            result *= double.Parse(current);
            current = result.ToString();
        }
        else if (latestOp == "/")
        {
            result /= double.Parse(current);
            current = result.ToString();
        }
        else
        {
            result = double.Parse(current);
        }
        
    }

    void OnGUI()
    {
        Debug.Log("GUI Rotation!");
        GUI.Button(new Rect(300, 50, 400, 50), current);
        if (GUI.Button(new Rect(300, 100, 100, 50), "7"))
        {
            if (current == "0" || !latestCh)
                current = "7";
            else
                current += "7";
            latestCh = true;
        }
        if(GUI.Button(new Rect(400, 100, 100, 50), "8"))
        {
            if (current == "0" || !latestCh)
                current = "8";
            else
                current += "8";
            latestCh = true;
        }
        if (GUI.Button(new Rect(500, 100, 100, 50), "9"))
        {
            if (current == "0" || !latestCh)
                current = "9";
            else
                current += "9";
            latestCh = true;
        }
        if (GUI.Button(new Rect(300, 150, 100, 50), "4"))
        {
            if (current == "0" || !latestCh)
                current = "4";
            else
                current += "4";
            latestCh = true;
        }
        if (GUI.Button(new Rect(400, 150, 100, 50), "5"))
        {
            if (current == "0" || !latestCh)
                current = "5";
            else
                current += "5";
            latestCh = true;
        }
        if (GUI.Button(new Rect(500, 150, 100, 50), "6"))
        {
            if (current == "0" || !latestCh)
                current = "6";
            else
                current += "6";
            latestCh = true;
        }
        if (GUI.Button(new Rect(300, 200, 100, 50), "1"))
        {
            if (current == "0" || !latestCh)
                current = "1";
            else
                current += "1";
            latestCh = true;
        }
        if (GUI.Button(new Rect(400, 200, 100, 50), "2"))
        {
            if (current == "0" || !latestCh)
                current = "2";
            else
                current += "2";
            latestCh = true;
        }
        if (GUI.Button(new Rect(500, 200, 100, 50), "3"))
        {
            if (current == "0" || !latestCh)
                current = "3";
            else
                current += "3";
            latestCh = true;
        }
        if (GUI.Button(new Rect(300, 250, 100, 50), "C"))
        {
            current = "0";
            result = 0;
            latestCh = true;
        }
        if (GUI.Button(new Rect(400, 250, 100, 50), "0"))
        {
            if (current != "0")
                current += "0";
            latestCh = true;
        }
           
        if (GUI.Button(new Rect(500, 250, 100, 50), "."))
        {
            if (!latestCh)
                current = "0";
            //如果字符串中已有小数点，则先将之前的小数点删去
            current = current.Replace(".", "");
            current += ".";
            latestCh = true;
        }

        if(GUI.Button(new Rect(600, 100, 100, 50), "+"))
        {
            calculate();
            latestOp = "+";
            latestCh = false;
        }
        if(GUI.Button(new Rect(600, 150, 100, 50), "-"))
        {
            calculate();
            latestOp = "-";
            latestCh = false;
        }
        if(GUI.Button(new Rect(600, 200, 100, 50), "*"))
        {
            calculate();
            latestOp = "*";
            latestCh = false;
        }
        if(GUI.Button(new Rect(600, 250, 100, 50), "/"))
        {
            calculate();
            latestOp = "/";
            latestCh = false;
        }

        if(GUI.Button(new Rect(300, 300, 100, 50), "Del"))
        {
            if (current.Length > 1)
            {
                current = current.Substring(0, current.Length - 1);
            }
            else
            {
                current = "0";
            }
        }
        if(GUI.Button(new Rect(400, 300, 300, 50), "="))
        {
            calculate();
            latestOp = "#";
            latestCh = false;
        }
    }
}
