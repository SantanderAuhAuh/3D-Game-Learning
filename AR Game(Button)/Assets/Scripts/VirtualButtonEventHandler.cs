//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
    public Material m_VirtualButtonMaterial;
    public GameObject fireDragon;

    private int left_len = 0, right_len = 0;

    void Start()
    {
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            vbs[i].RegisterEventHandler(this);

            if (m_VirtualButtonMaterial != null)
            {
                vbs[i].GetComponent<MeshRenderer>().sharedMaterial = m_VirtualButtonMaterial;
            }
        }
    }

    void Update()
    {
        if (left_len > 0)
        {
            fireDragon.transform.Translate(Vector3.left * Time.deltaTime, Space.World);
            left_len--;
        }
        if (right_len > 0)
        {
            fireDragon.transform.Translate(Vector3.right * Time.deltaTime, Space.World);
            right_len--;
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("Virtual button " + vb.VirtualButtonName + " pressed");

        if (vb.VirtualButtonName.Contains("GoUpBtn"))
        {
            left_len += 200;
        }
        else if (vb.VirtualButtonName.Contains("GoDownBtn"))
        {
            right_len += 200;
        }
        // fireDragon.SetActive(false);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("Virtual button released");
    }
}