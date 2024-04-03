using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    void ResetShooting()
    {
        _playerController.canShoot = true;
        _animator.Play("Idle");
    }

    void CameraStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
