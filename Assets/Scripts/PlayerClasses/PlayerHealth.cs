using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int m_Health = 100;
    [SerializeField]
    private Slider health_Slider;

    private GameObject _uI_holder;

    private void Start()
    {
        health_Slider = GameObject.Find("Bullet Slider").GetComponent<Slider>();
        health_Slider.value = m_Health;

        _uI_holder = GameObject.Find("Holder");
    }

    public void ApplyDamage(int damageAmount)
    {
        m_Health -= damageAmount;

        if(m_Health < 0)
        {
            m_Health = 0;
        }

        health_Slider.value = m_Health;

        if(m_Health == 0)
        {
            _uI_holder.SetActive(false);
            GameplayController.Instance.GameOver();
        }
    }


}
