using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaConllider : MonoBehaviour
{

    public int sign = 0;
    SceneController sceneController;

    // Use this for initialization
    void Start()
    {
        sceneController = SSDirector.getInstance().currentSceneController as SceneController;
    }

    private void Update()
    {
        sceneController = SSDirector.getInstance().currentSceneController as SceneController;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Area:"+other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            sceneController.areaSign = sign;
        }
    }

}