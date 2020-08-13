using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosix;
    public Vector2 minPosix;

    void LateUpdate(){
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            // targetPosition.x = Mathf.Clamp(targetPosition.x, minPosix.x, maxPosix.x);
            // targetPosition.y = Mathf.Clamp(targetPosition.y, minPosix.y, maxPosix.y);

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                smoothing);
    }
}
