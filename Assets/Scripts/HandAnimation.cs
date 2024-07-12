using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction{
    clockwise = 1,
    counterclockwise = -1
}

public class HandAnimation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_delay;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_radius;
    [SerializeField] private Direction m_direction;

    [SerializeField] private Transform m_transform;

    private float m_time;
    private void Start()
    {
        m_time = m_delay * (float)m_direction;
    }
    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime * m_speed * (float)m_direction;

        float x = Mathf.Cos(m_time) * m_radius;
        float y = Mathf.Sin(m_time) * m_radius;
        float z = 0;

        transform.position = new Vector3(m_transform.position.x + x, m_transform.position.y + y, z);
    }
}
