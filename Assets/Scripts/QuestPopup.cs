using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopup : MonoBehaviour
{
	public static QuestPopup instance;

	[SerializeField] Image m_demonImage;
	Animator m_animator;
	float m_timer = 0;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		instance = this;
	}

	private void Update()
	{
		if (m_timer > 1.5f)
		{
			HideQuest();
		}
		else
		{
			m_timer += Time.unscaledDeltaTime;
		}
	}

	public void ShowQuest(Sprite image)
	{
		m_timer = 0;
		m_animator.SetBool("Show", true);
		m_demonImage.sprite = image;
	}

	public void HideQuest()
	{
		m_animator.SetBool("Show", false);
	}
}
