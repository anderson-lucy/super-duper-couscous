using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float offsetX;
    public float offsetY;
    private Rigidbody2D playerRB2D;

    public float globalMinX;
    public float globalMaxX;
    public float globalMinY;
    public float globalMaxY;

    private Vector3 smoothPos;
    public float smoothSpeed = 0.5f;

    void Start()
    {
        playerRB2D = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Vector3 pos = transform.position;
        //Vector2 playerVel = playerRB2D.velocity;

        //if (playerVel.x > 0.0f)
        //{
        //    pos.x = player.position.x + offsetX;
        //}
        //else if (playerVel.x < 0.0f)
        //{
        //    pos.x = player.position.x; //- offsetX;
        //}

        //pos.y = player.position.y + offsetY;

        //float maxX = globalMaxX - Camera.main.orthographicSize * Camera.main.aspect;
        //float maxY = globalMaxY - Camera.main.orthographicSize;
        //float minX = globalMinX + Camera.main.orthographicSize * Camera.main.aspect;
        //float minY = globalMinY + Camera.main.orthographicSize;

        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        //smoothPos = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, pos.z), smoothSpeed);

        //transform.position = smoothPos;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        Vector2 playerVel = playerRB2D.velocity;

        if (playerVel.x > 0.0f)
        {
            pos.x = player.position.x + offsetX;
        }
        else if (playerVel.x < 0.0f)
        {
            pos.x = player.position.x - offsetX;
        }

        pos.y = player.position.y + offsetY;

        float maxX = globalMaxX - Camera.main.orthographicSize * Camera.main.aspect;
        float maxY = globalMaxY - Camera.main.orthographicSize;
        float minX = globalMinX + Camera.main.orthographicSize * Camera.main.aspect;
        float minY = globalMinY + Camera.main.orthographicSize;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        smoothPos = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, pos.z), smoothSpeed);

        transform.position = smoothPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0.0f), new Vector3(globalMaxX, globalMinY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMaxY, 0.0f), new Vector3(globalMaxX, globalMaxY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0.0f), new Vector3(globalMinX, globalMaxY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMaxX, globalMinY, 0.0f), new Vector3(globalMaxX, globalMaxY, 0.0f));
    }
}
