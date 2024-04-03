using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeExplosion : MonoBehaviour
{
    public float timer = 2f;

    private void Start()
    {
        Invoke(nameof(DeactiveGameObject), timer);
    }

    private void DeactiveGameObject()
    {
        gameObject.SetActive(false);
    }

}
