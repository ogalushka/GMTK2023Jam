using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraMovementSpeed = 5f;
    public float screenEdgeDistanceToMove = 20f;

    [SerializeField] private BoxCollider2D boundsBox;
    private float halfHeight, halfWidth;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 movement = Vector3.zero;

        if (mousePosition.x < screenEdgeDistanceToMove)
        {
            movement += Vector3.left;
        }
        else if (mousePosition.x > Screen.width - screenEdgeDistanceToMove)
        {
            movement += Vector3.right;
        }

        if (mousePosition.y < screenEdgeDistanceToMove)
        {
            movement += Vector3.down;
        }
        else if (mousePosition.y > Screen.height - screenEdgeDistanceToMove)
        {
            movement += Vector3.up;
        }

        movement.Normalize();
        movement *= cameraMovementSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight);
        transform.position = newPosition;
    }

}
