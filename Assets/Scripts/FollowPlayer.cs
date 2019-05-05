using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public Vector3 offset;
    public Vector3 angleOffset;
    public Transform transitionPoint;

    private bool isTranslated;


    public float moveSmoothSpeed = 2.0f;
    public float angleSmoothSpeed = 2.0f;

    void Start() {
        isTranslated = false;
    }

    // Update is called once per frame
    void LateUpdate () {

        Vector3 dist = player.position - transitionPoint.position;
        if (!isTranslated && dist.sqrMagnitude < 2.0f) {
            isTranslated = true;
            offset.x += 7f;
            offset.y += 5f;
        }

        //Smooth camera movement
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, moveSmoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        //Smooth camera rotation to follow player
        Quaternion currentAngle = transform.rotation;

        player.position += angleOffset;
        transform.LookAt(player);
        Quaternion desiredAngle = transform.rotation;
        player.position -= angleOffset;

        Quaternion smoothedAngle = Quaternion.Lerp(currentAngle, desiredAngle, angleSmoothSpeed * Time.deltaTime);

        transform.rotation = smoothedAngle;
	}
}
