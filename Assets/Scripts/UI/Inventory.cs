using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Inventory : MonoBehaviour
{
	public static Inventory instance;

	[SerializeField] private GameObject m_coinPopup;
	[SerializeField] private GameObject m_sinnerPopup;
	[SerializeField] private GameObject canvas;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	[Header("Sinner's number")]
	public int angerNumber = 0;
	public int gluttonyNumber = 0;
	public int greedNumber = 0;
	public int lustNumber = 0;
	public int prideNumber = 0;
	public int envyNumber = 0;
	public int slothNumber = 0;

	[Header("Other")]
	public int soulsNumber = 0;

	[Header("GameObjects")]
	[SerializeField] private Menu m_menu;

	public int GetSinnersNumber(SinType type)
	{
		switch (type)
		{
			case SinType.Anger:
				return angerNumber;

			case SinType.Gluttony:
				return gluttonyNumber;

			case SinType.Pride:
				return prideNumber;

			case SinType.Greed:
				return greedNumber;

			case SinType.Lust:
				return lustNumber;

			case SinType.Envy:
				return envyNumber;

			case SinType.Sloth:
				return slothNumber;

			default:
				return 0;
		}
	}

	public void ChangeSinnersNumber(SinType type, int value)
	{
		switch(type)
		{
			case SinType.Anger:
				angerNumber += value;
				break;

			case SinType.Gluttony:
				gluttonyNumber += value;
				break;

			case SinType.Pride:
				prideNumber += value;
				break;

			case SinType.Greed:
				greedNumber += value;
				break;

			case SinType.Lust:
				lustNumber += value;
				break;

			case SinType.Envy:
				envyNumber += value;
				break;

			case SinType.Sloth:
				slothNumber += value;
				break;

			default:
				break;
		}
	}

	public void ChangeSoulsNumber(int value)
	{
		soulsNumber += value;
	}

	public void CreateCoinPopup(Transform coin, int value)
	{
		GameObject popup = Instantiate(m_coinPopup);
		popup.transform.SetParent(canvas.transform, false);
		popup.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(coin.position);
		popup.GetComponent<CoinPopup>().Setup(value);
	}

	public void CreateSinnerPopup(Transform sinner, int value)
	{
		GameObject popup = Instantiate(m_sinnerPopup);
		popup.transform.SetParent(canvas.transform, false);
		popup.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(sinner.position);
		popup.GetComponent<CoinPopup>().Setup(value);
	}
}
