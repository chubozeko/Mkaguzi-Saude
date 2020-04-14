using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraTarget;
    public float cameraSpeed;

    private void FixedUpdate()
    {
        if (cameraTarget != null)
        {
            var newPos = Vector2.Lerp(transform.position,
            cameraTarget.position,
            Time.deltaTime * cameraSpeed);

            transform.position = new Vector3(newPos.x, newPos.y, -10f);
        }
    }
}
