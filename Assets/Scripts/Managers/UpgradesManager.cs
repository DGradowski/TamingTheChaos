using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
	public static UpgradesManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	[Header("Upgrades")]
	public int maxCombo = 0;
	public int basicValue = 0;
	public int healthBonus = 0;
	public int extraTime = 0;
	public int soulsMagnet = 0;

	[Header("Upgrades Costs")]
	public int[] c_maxCombo;
	public int[] c_basicValue;
	public int[] c_healthBonus;
	public int[] c_extraTime;
	public int[] c_soulsMagnet;

	[Header("Upgrades Names")]
	public string n_maxCombo;
	public string n_basicValue;
	public string n_healthBonus;
	public string n_extraTime;
	public string n_soulsMagnet;

	[Header("Upgrades Description")]
	[TextArea(2, 5)] public string d_maxCombo;
	[TextArea(2, 5)] public string d_basicValue;
	[TextArea(2, 5)] public string d_healthBonus;
	[TextArea(2, 5)] public string d_extraTime;
	[TextArea(2, 5)] public string d_soulsMagnet;

	private int m_currentUpgrade = 0;

	public void ChangeUpgradeNuber(int i)
	{
		m_currentUpgrade += i;
		if (m_currentUpgrade < 0) m_currentUpgrade = 5 - 1;
		if (m_currentUpgrade >= 5) m_currentUpgrade = 0;
		SoundFXManager.instance.PlayMenuSound(FindAnyObjectByType<PlayerMovement>().transform, 1f);
		Menu.instance.UpdateMenuValues();
	}

	public void BuyUpgrade()
	{
		if (!CanBuyUpgrade()) return;
		switch (m_currentUpgrade)
		{
			case 0:
				Inventory.instance.ChangeSoulsNumber(c_maxCombo[maxCombo] * -1);
				maxCombo++;
				break;
			case 1:
				Inventory.instance.ChangeSoulsNumber(c_basicValue[basicValue] * -1);
				basicValue++;
				break;
			case 2:
				Inventory.instance.ChangeSoulsNumber(c_healthBonus[healthBonus] * -1);
				healthBonus++;
				break;
			case 3:
				Inventory.instance.ChangeSoulsNumber(c_extraTime[extraTime] * -1);
				extraTime++;
				break;
			case 4:
				Inventory.instance.ChangeSoulsNumber(c_soulsMagnet[soulsMagnet] * -1);
				soulsMagnet++;
				break;
		}
		SoundFXManager.instance.PlayMenuSound(FindAnyObjectByType<PlayerMovement>().transform, 1f);
		Menu.instance.UpdateMenuValues();
	}

	public string GetDescription()
	{
		switch (m_currentUpgrade)
		{
			case 0:
				return d_maxCombo;
			case 1:
				return d_basicValue;
			case 2:
				return d_healthBonus;
			case 3:
				return d_extraTime;
			case 4:
				return d_soulsMagnet;
			default:
				return "";
		}
	}

	public string GetName()
	{
		switch (m_currentUpgrade)
		{
			case 0:
				return n_maxCombo;
			case 1:
				return n_basicValue;
			case 2:
				return n_healthBonus;
			case 3:
				return n_extraTime;
			case 4:
				return n_soulsMagnet;
			default:
				return "";
		}
	}

	public int GetProgress()
	{
		switch (m_currentUpgrade)
		{
			case 0:
				return maxCombo;
			case 1:
				return basicValue;
			case 2:
				return healthBonus;
			case 3:
				return extraTime;
			case 4:
				return soulsMagnet;
			default:
				return 3;
		}
	}

	public int GetCost()
	{
		switch (m_currentUpgrade)
		{
			case 0:
				return c_maxCombo[maxCombo];
			case 1:
				return c_basicValue[basicValue];
			case 2:
				return c_healthBonus[healthBonus];
			case 3:
				return c_extraTime[extraTime];
			case 4:
				return c_soulsMagnet[soulsMagnet];
			default:
				return 3;
		}
	}

	public bool CanBuyUpgrade()
	{
		switch (m_currentUpgrade)
		{
			case 0:
				if (maxCombo >= 3) return false;
				if (c_maxCombo[maxCombo] > Inventory.instance.soulsNumber) return false;
				break;
			case 1:
				if (basicValue >= 3) return false;
				if (c_basicValue[basicValue] > Inventory.instance.soulsNumber) return false;
				break;
			case 2:
				if (healthBonus >= 3) return false;
				if (c_healthBonus[healthBonus] > Inventory.instance.soulsNumber) return false;
				break;
			case 3:
				if (extraTime >= 3) return false;
				if (c_extraTime[extraTime] > Inventory.instance.soulsNumber) return false;
				break;
			case 4:
				if (soulsMagnet >= 3) return false;
				if (c_soulsMagnet[soulsMagnet] > Inventory.instance.soulsNumber) return false;
				break;
			default:
				return false;
		}
		return true;
	}
}
