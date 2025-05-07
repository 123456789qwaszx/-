using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaincam : MonoBehaviour
{
    [SerializeField]
    Vector2 minCameraBoundary;
    [SerializeField]
    Vector2 maxCameraBoundary;
    [SerializeField]
    float smoothing = 0.2f;

    GameObject player;

    
    private void FixedUpdate()
    {
        player = GameObject.Find("Player");
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
