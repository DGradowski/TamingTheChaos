using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class TamingArea : MonoBehaviour
{
	[SerializeField] private Hands m_hands;
	private RectTransform m_rectTransform;
	private bool m_active;
	private float m_timer;
	[SerializeField] private float m_tamingTime;
	[SerializeField] private int m_stages;
	private int m_currentStage = 0;

	private int m_eyesToClick = 0;
	private int m_extraEyes = 0;
	private int m_clickedEyes = 0;

	[SerializeField] GameObject m_eye;

	private float m_min_width;
	private float m_max_width;
	private float m_min_height;
	private float m_max_height;

	// Update is called once per frame
	void Update()
	{
		m_timer += Time.deltaTime;
        if (m_tamingTime / m_stages <= m_timer)
		{
			m_currentStage++;
            m_eye.GetComponent<TamingPoint>().ChangeStage(m_currentStage);
			m_timer = 0;
        }
    }

	public void StartTaming(int min_eyes, int extra, float taming_time)
	{
		m_clickedEyes = -1;
		m_eyesToClick = min_eyes;
		m_extraEyes = extra;
		m_tamingTime = taming_time;
		ShowNewEye();
		
	}

	public void ShowNewEye()
	{
		m_active = true;
		m_eye.GetComponent<TamingPoint>().ChangeStage(0);
		GenerateNewPosition();
        m_timer = 0;
		m_currentStage = 0;
		m_clickedEyes++;
        if (m_clickedEyes > m_eyesToClick + m_extraEyes)
        {
            StopTaming();
        }
    }

	public void GenerateNewPosition()
	{
        m_rectTransform = GetComponent<RectTransform>();

        float eye_width = m_eye.GetComponent<RectTransform>().rect.width;
        float eye_height = m_eye.GetComponent<RectTransform>().rect.height;

        m_max_width = m_rectTransform.rect.width - eye_width;
        m_max_height = m_rectTransform.rect.height - eye_height;

        m_min_width = 0 + eye_width;
        m_min_height = 0 + eye_height;

        float x_pos = Random.value * (m_max_width - m_min_width) + m_min_width;
        float y_pos = Random.value * (m_max_height - m_min_height) + m_min_height;

        m_eye.transform.position = new Vector3(x_pos, y_pos, 0);
    }

	public void StopTaming()
	{
		m_hands.ReleaseEnemy();
		m_hands.EnableWeapon();
		gameObject.SetActive(false);
	}
}
