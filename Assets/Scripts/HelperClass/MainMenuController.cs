using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Animator _cameraAnim;

    public void PlayGame()
    {
        _cameraAnim.Play("CameraSlide");
    }

}
