using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    [SerializeField] private Camera mainCamera;
    // Update is called once per frame
    void Update()
    {  
        float player1X = player1.position.x;
        float player2X = player2.position.x;
        float player1Y = player1.position.y;
        float player2Y = player2.position.y;
        mainCamera.orthographicSize = Mathf.Abs(player1X - player2X)/2.2f;
        gameObject.transform.position = new Vector3((player1X + player2X)/2-4, (player1Y + player2Y)/2, 0f);
        if (mainCamera.orthographicSize <= 3) {
            mainCamera.orthographicSize = 3;
        }
        if (mainCamera.orthographicSize >= 6) {
            mainCamera.orthographicSize = 6;
        }
    }
}
