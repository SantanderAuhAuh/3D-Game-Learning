using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button2_click : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        // 绑定监听器
        btn.onClick.AddListener(TaskOnclick);
    }

    void TaskOnclick()
    {
        // 加载Build Settings中的索引对应的Scene
        Application.LoadLevel(2);
    }
}
