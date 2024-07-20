using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ComboText : MonoBehaviour
{
    TMPro.TextMeshProUGUI m_text;
    Animator m_animator;
    RectTransform m_rectTransform;

    float m_startingX;
    float m_startingY;

    [SerializeField] float m_xSpeed;
    [SerializeField] float m_ySpeed;

    [SerializeField] float m_baseRange;
    [SerializeField] float m_baseSpeed;

    float m_range;
    float m_speed;

    float m_timer = 0;

    private void Start()
    {
        m_text = GetComponent<TMPro.TextMeshProUGUI>();
        m_rectTransform = GetComponent<RectTransform>();
        m_startingX = m_rectTransform.anchoredPosition.x;
        m_startingY = m_rectTransform.anchoredPosition.y;
        m_text.text = "";
    }

    void Update()
    {
        m_timer += Time.deltaTime;
        float x = m_startingX + Mathf.Sin(m_timer * m_xSpeed * m_speed + 40) * m_range;
        float y = m_startingY + Mathf.Sin(m_timer * m_ySpeed * m_speed) * m_range;
        Vector3 pos_vector = new Vector3(x, y, 0);
        m_rectTransform.anchoredPosition = pos_vector;
    }

    public void UpdateText(int combo)
    {
        if (combo <= 0)
        {
            m_text.text = "";
            return;
        }
        m_range = m_baseRange * combo;
        if (m_range > 30) m_range = 30;
        m_speed = m_baseSpeed * combo;
        m_text.text = "x " + (combo + 1).ToString();
    }
}
