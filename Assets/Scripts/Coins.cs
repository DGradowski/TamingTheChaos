using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
	public static Coins instance;

    [SerializeField] TextMeshProUGUI m_text;
    Animator m_animator;
	float m_timer = 0;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		instance = this;
	}

	private void Update()
	{
		if (m_timer > 1f)
		{
			HideCoins();
		}
		else
		{
			m_timer += Time.unscaledDeltaTime;
		}
	}

	public void ShowCoins()
	{
		m_timer = 0;
		m_animator.SetBool("Show", true);
		m_text.text = Inventory.instance.soulsNumber.ToString();
	}

	public void HideCoins()
	{
		m_animator.SetBool("Show", false);
	}
}
