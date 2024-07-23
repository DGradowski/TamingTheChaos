using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Request : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Sprite[] m_sprites;
	[SerializeField] Image m_image;
	[SerializeField] TMPro.TextMeshProUGUI m_text;
	[SerializeField] GameObject m_infoPanel;
	[SerializeField] TMPro.TextMeshProUGUI m_infoText;
	[SerializeField] ContentFitterRefresh m_contentFitterRefresh;

	[SerializeField] string[] m_sinAdjectives;
	string m_sinAdjective;
	int m_quantity;

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_infoPanel.SetActive(true);
		m_contentFitterRefresh.RefreshContentFitters();
		if (m_sinAdjective == "None") m_infoText.text = "Capture " + m_quantity.ToString() + " sinners";
		else m_infoText.text = "Capture " + m_quantity.ToString() + " " + m_sinAdjective + " sinners";
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_infoPanel.SetActive(false);
	}

	public void SetRequest(SinType sinType, int quantity)
	{
		m_image.sprite = m_sprites[(int)sinType];
		m_text.text = quantity.ToString();
		m_quantity = quantity;
		if (sinType == SinType.None) m_sinAdjective = "None";
		else m_sinAdjective = m_sinAdjectives[(int)sinType];
	}
}
