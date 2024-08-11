using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager instance;
    private int m_possibleQuests = 0;
    private int m_possibleUpgrades = 0;

    [Header("Alerts")]
    [SerializeField] GameObject m_mainAlert;
    [SerializeField] GameObject m_questsAlert;
    [SerializeField] GameObject m_shopAlert;

	private void Awake()
	{
		instance = this;
		m_mainAlert.SetActive(false);
		m_questsAlert.SetActive(false);
		m_shopAlert.SetActive(false);

	}

	public void CheckQuests()
    {
        m_possibleQuests = 0;

        for (int i = 0; i < QuestManager.instance.quests.Length; i++)
        {
            if (!QuestManager.instance.quests[i].inProgress) continue;
            if (QuestManager.instance.m_completedQuests < 7 && i == 7) continue;
            if (QuestManager.instance.IsRequestCompleted(i)) m_possibleQuests++;
        }
    }

    public void CheckUpgrades()
    {
        m_possibleUpgrades = 0;
        int souls = Inventory.instance.soulsNumber;
        UpgradesManager upgrades = UpgradesManager.instance;
        if (upgrades.maxCombo < 3 && souls >= upgrades.c_maxCombo[upgrades.maxCombo]) m_possibleUpgrades++;
		if (upgrades.basicValue < 3 && souls >= upgrades.c_basicValue[upgrades.basicValue]) m_possibleUpgrades++;
		if (upgrades.healthBonus < 3 && souls >= upgrades.c_healthBonus[upgrades.healthBonus]) m_possibleUpgrades++;
		if (upgrades.extraTime < 3 && souls >= upgrades.c_extraTime[upgrades.extraTime]) m_possibleUpgrades++;
		if (upgrades.soulsMagnet < 3 && souls >= upgrades.c_soulsMagnet[upgrades.soulsMagnet]) m_possibleUpgrades++;
    }

    public void ShowAlerts()
    {
        CheckQuests();
        CheckUpgrades();
        if (m_possibleQuests + m_possibleUpgrades > 0) m_mainAlert.SetActive(true);
        else m_mainAlert.SetActive(false);

        if (!Menu.instance.menuIsActive) return;
        if (m_possibleQuests > 0) m_questsAlert.SetActive(true);
        else m_questsAlert.SetActive(false);

		if (m_possibleUpgrades > 0) m_shopAlert.SetActive(true);
		else m_shopAlert.SetActive(false);
	}

}
