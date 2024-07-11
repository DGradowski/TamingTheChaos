using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float m_maxHP;
    [SerializeField] private float m_damage;

    private float m_currentHP;
    // Start is called before the first frame update
    void Start()
    {
        m_currentHP = m_maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDamage() { return m_damage; }
}
