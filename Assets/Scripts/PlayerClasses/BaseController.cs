using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Vector3 _speed;

    [SerializeField]
    private float x_Speed = 8f; // left and right speed
    [SerializeField]
    private float z_Speed = 15f; // going forward
    [SerializeField]
    private float _accelerator = 15f; // go faster
    [SerializeField]
    private float _deaccelerator = 10f; // brake

    protected float _rotationSpeed = 10f; // rotate speed
    protected float _maximumAngle = 10f;

    [SerializeField]
    private float low_sound_pitch, normal_Sound_Pitch, high_Sound_Pitch; // simulate engine sounds

    [SerializeField]
    private AudioClip engine_On_Sound, engine_off_Speed; //engine start and stop sound
   
    private bool _isSlow; //if we are going slow or not
    private AudioSource soundManager;

    private void Awake()
    {
            soundManager = GetComponent<AudioSource>();

    }

    void Start()
    {
        _speed = new Vector3(0f, 0f, z_Speed);
    }

    //dividing by 2 so it does not go very fast
    protected void MoveLeft()
    {
        _speed = new Vector3(-x_Speed / 2f, 0f, _speed.z);
    }

    protected void MoveRight()
    {
        _speed = new Vector3(x_Speed / 2f, 0f, _speed.z);
    }

    protected void MoveStraight()
    {
        _speed = new Vector3(0f, 0f, _speed.z);
    }

    protected void MoveNormal()
    {
        if(_isSlow)
        {
            _isSlow = false;
            soundManager.clip = engine_On_Sound;
            soundManager.volume = .3f;
            soundManager.Play();
        }
        _speed = new Vector3(_speed.x, 0f, z_Speed);
    }

    protected void MoveSlow()
    {
        if (!_isSlow)
        {
            _isSlow = true;
            soundManager.Stop();
            soundManager.clip = engine_off_Speed;
            soundManager.volume = .5f;
            soundManager.Play();
        }
        _speed = new Vector3(_speed.x, 0f, _deaccelerator);
    }

    protected void MoveFast()
    {
        if (!_isSlow)
        {
            _isSlow = true;
            soundManager.Stop();
            soundManager.clip = engine_On_Sound;
            soundManager.volume = .3f;
            soundManager.Play();
        }
        _speed = new Vector3(_speed.x, 0f, _accelerator);
    }
}
