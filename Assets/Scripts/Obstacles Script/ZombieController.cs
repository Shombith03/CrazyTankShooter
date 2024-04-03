using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodFX;
    private float _speed;
    private Rigidbody _rigidbody;

    private bool isAlive;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        isAlive = true;
        _speed = Random.Range(0, 5);
    }

    private void Update()
    {
        if(isAlive)
        {
            _rigidbody.velocity = new Vector3(0f, 0f, -_speed);
        }
        // if zombies are not killed 
        if (transform.position.y < -10f)
        {
            gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        isAlive = false;
        _rigidbody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle");

        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // rotate them falling fown
        transform.localScale = new Vector3(1f, 1f, 0.2f); //change their scale
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    private void DeactivateZombie()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(_bloodFX, transform.position, Quaternion.identity);
            Invoke(nameof(DeactivateZombie), 3f);
            GameplayController.Instance.IncreaseScore();
            Die();  
        }
    }
}
