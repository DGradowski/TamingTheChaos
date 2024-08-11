using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorChangeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Button x = GetComponent<Button>();
		if (x != null)
		{
			if (x.interactable == false) return;
		}
		CursorManager.instance.SetHoverCursor();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		CursorManager.instance.SetNormalCursor();
	}
}
