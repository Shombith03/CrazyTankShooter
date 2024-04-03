using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private int _damage = 20;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            // deal damage
            collision.gameObject.GetComponent<PlayerHealth>().ApplyDamage(_damage);

            gameObject.SetActive(false);
        }

        if(collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
