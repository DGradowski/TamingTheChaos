using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Logo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject m_credits;

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_credits.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_credits.SetActive(false);
	}
}
