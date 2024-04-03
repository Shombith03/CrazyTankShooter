using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _cameraDistance;
    [SerializeField]
    private float _height = 0.5f;
    [SerializeField]
    private float _heightDamping = 3.25f;
    [SerializeField]
    private float _rotationDumping = 0.27f;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        
    }

    //called after update and fixedupdate
    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        float wanted_Rotation = _target.eulerAngles.y;
        float wanted_Height= _target.position.y + _height; // how high above the player

        float current_RotationAngle = transform.eulerAngles.y;
        float current_Height = transform.position.y;

        current_RotationAngle = Mathf.LerpAngle(current_RotationAngle, wanted_Rotation, _rotationDumping * Time.deltaTime);

        current_Height = Mathf.Lerp(current_Height, wanted_Height, _heightDamping * Time.deltaTime);

        Quaternion current_Rotation = Quaternion.Euler(0f, current_RotationAngle, 0f); 

        transform.position = _target.position;
        transform.position -= current_Rotation * Vector3.forward * _cameraDistance; // set the distance away from target

        transform.position = new Vector3(transform.position.x, current_Height, transform.position.z);
    }

}
