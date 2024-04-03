using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _bulletStartPoint;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private ParticleSystem _shootFX;

    private Animator _shootSliderAnim;
    [HideInInspector]
    public bool canShoot;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _shootSliderAnim = GameObject.Find("Bullet Slider").GetComponent<Animator>();
        GameObject.Find("ShootButton").GetComponent<Button>().onClick.AddListener(ShootControl);
        canShoot = true;
    }

    private void Update()
    {
        ControlMovementKeyboard();
        ChangeRotation();
    }

    private void FixedUpdate()
    {
        MoveTank();
    }

    private void MoveTank()
    {
        _rigidbody.MovePosition(_rigidbody.position + _speed * Time.deltaTime);
    }

    private void ControlMovementKeyboard()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }

        //when key is released
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveNormal();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            MoveNormal();
        }
    }

    private void ChangeRotation()
    {
        if(_speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, _maximumAngle, 0f), Time.deltaTime * _rotationSpeed); // rotate to negative max angle
        }
        else if(_speed.x < 0) // rotate to positive max angle
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -_maximumAngle, 0f), Time.deltaTime * _rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * _rotationSpeed); // rotate to normal angle
        }
    }

    public void ShootControl()
    {
        if(Time.timeScale != 0)
        {
            if(canShoot)
            {
                GameObject bullet = Instantiate(_bulletPrefab, _bulletStartPoint.position, Quaternion.identity);
                bullet.GetComponent<BulletController>().Move(2000f);
                _shootFX.Play();

                // call anim

                canShoot = false;
                _shootSliderAnim.Play("FillInShootBar");
            }
        }

    }
}
