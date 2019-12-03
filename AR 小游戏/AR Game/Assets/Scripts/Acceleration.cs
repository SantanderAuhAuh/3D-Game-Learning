using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    public float speed = 200;

    static bool isTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        isTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            isTouch = true;
        }
        if (isTouch)
        {
            Vector3 move = new Vector3(Input.acceleration.x * speed * Time.deltaTime, Input.acceleration.y * Time.deltaTime);
            transform.Translate(move);
        }
    }
}
