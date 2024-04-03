using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;

    public void Move(float speed)
    {
        _rigidBody.AddForce(transform.forward.normalized * speed);
        Invoke(nameof(DeactivateBullet), 5f);
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            DeactivateBullet();
        }
    }

}
