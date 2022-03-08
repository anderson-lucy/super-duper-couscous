using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float offsetX;
    public float offsetY;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = player.position.x + offsetX;
        pos.y = player.position.y + offsetY;
        transform.position = pos;
    }
}
