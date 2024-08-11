using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPopup : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshPro;
	private RectTransform m_RectTransform;

	[SerializeField] float m_lifeTime;
	float m_timeRemaining;
	[SerializeField] float m_speed;

	private void Awake()
	{
		m_TextMeshPro = GetComponent<TextMeshProUGUI>();
		m_timeRemaining = m_lifeTime;
		m_RectTransform = GetComponent<RectTransform>();
	}

	public void Setup(int valueText)
    {
		m_TextMeshPro.text = "+" + valueText.ToString();
    }

	private void Update()
	{
		if (m_timeRemaining >= 0)
		{
			m_timeRemaining -= Time.deltaTime;
			m_RectTransform.anchoredPosition += new Vector2(0, Time.deltaTime * m_speed);
			if (m_timeRemaining < m_lifeTime / 2)
			{
				float value = m_timeRemaining / (m_lifeTime / 2);

				m_TextMeshPro.color = new Color(1, 1, 1, value);

				transform.localScale = new Vector3(value, value, value);
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
