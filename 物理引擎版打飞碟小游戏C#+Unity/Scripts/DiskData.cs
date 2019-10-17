using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskData : MonoBehaviour
{
    private Vector3 size;
    private Color color;
    private float speed;
    private Vector3 direction;

    public DiskData() { }

    public Vector3 getSize()
    {
        return size;
    }

    public float getSpeed()
    {
        return speed;
    }

    public Vector3 getDirection()
    {
        return direction;
    }

    public Color getColor()
    {
        return color;
    }

    public void setDiskData(Vector3 size, Color color, float speed, Vector3 direction)
    {
        this.size = size;
        this.color = color;
        this.speed = speed;
        this.direction = direction;
    }
}