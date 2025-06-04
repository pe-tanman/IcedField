using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector2 offset;
    bool isTrackingEnabled = true;

    void Start()
    {
        StartCoroutine(moveCamera());
    }

    void Update()
    {
        if (player != null && isTrackingEnabled)
        {
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
    private IEnumerator moveCamera()
    {
        yield return new WaitForSeconds(60f);
        isTrackingEnabled = false;
        transform.position = new Vector3(0, 0, -10);
        Camera.main.orthographicSize = 23;
    }
}
