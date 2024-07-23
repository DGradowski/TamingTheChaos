using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	[SerializeField] public Quest[] quests;
	[SerializeField] public int m_questNumber = 0;
	[SerializeField] public bool extraQuestIsAdded = false;
	[SerializeField] public int m_completedQuests = 0;
	[SerializeField] public Fork fork;
	[SerializeField] public WeaponSelection m_weaponSelection;


	public void ChangeQuestNuber(int i)
	{
		m_questNumber += i;
		if (extraQuestIsAdded)
		{
			if (m_questNumber < 0) m_questNumber = quests.Length - 1;
			if (m_questNumber >= quests.Length) m_questNumber = 0;
		}
		else
		{
			if (m_questNumber < 0) m_questNumber = quests.Length - 2;
			if (m_questNumber >= quests.Length - 1) m_questNumber = 0;
		}
		
		SoundFXManager.instance.PlayMenuSound(FindAnyObjectByType<PlayerMovement>().transform, 1f);
		Menu.instance.UpdateMenuValues();
	}

	public void CompleteQuest()
	{
		if (quests[m_questNumber].inProgress)
		{
			if (IsRequestCompleted())
			{
				if (m_questNumber == quests.Length - 1)
				{
					int requirment = quests[m_questNumber].requestedQuantity[0];
					while (requirment > 0)
					{
						int rand = Random.Range(0, 7);
						if (Inventory.instance.GetSinnersNumber((SinType)rand) <= 0) continue;
						Inventory.instance.ChangeSinnersNumber((SinType)rand, -1);
						requirment--;
					}
				}
				else
				{
					for (int i = 0; i < quests[m_questNumber].requestedType.Length; i++)
					{
						SinType requestedType = quests[m_questNumber].requestedType[i];
						int requestedQuantity = quests[m_questNumber].requestedQuantity[i];
						Inventory.instance.ChangeSinnersNumber(requestedType, requestedQuantity * -1);
					}
				}
				Inventory.instance.ChangeSoulsNumber(quests[m_questNumber].reward);
				quests[m_questNumber].inProgress = false;
				m_completedQuests++;
				if (m_completedQuests >= quests.Length - 1) extraQuestIsAdded = true;
				if (m_questNumber == quests.Length - 1)
				{
					m_weaponSelection.SelectWeapon(0);
					fork.UpgradeFork();
				}
			}
		}
		Menu.instance.UpdateMenuValues();
	}

	public bool IsRequestCompleted()
	{
		if (m_questNumber == quests.Length - 1)
		{
			int allSinners = Inventory.instance.GetSinnersNumber(SinType.Anger);
			allSinners += Inventory.instance.GetSinnersNumber(SinType.Lust);
			allSinners += Inventory.instance.GetSinnersNumber(SinType.Gluttony);
			allSinners += Inventory.instance.GetSinnersNumber(SinType.Sloth);
			allSinners += Inventory.instance.GetSinnersNumber(SinType.Pride);
			allSinners += Inventory.instance.GetSinnersNumber(SinType.Envy);
			allSinners += Inventory.instance.GetSinnersNumber(SinType.Greed);
			if (allSinners < quests[m_questNumber].requestedQuantity[0]) return false;
		}
		else
		{
			for (int i = 0; i < quests[m_questNumber].requestedType.Length; i++)
			{
				SinType requestedType = quests[m_questNumber].requestedType[i];
				int requestedQuantity = quests[m_questNumber].requestedQuantity[i];

				if (Inventory.instance.GetSinnersNumber(requestedType) < requestedQuantity) return false;
			}
		}
		return true;
	}

	public Quest GetCurrentQuest()
	{
		if (m_questNumber >= quests.Length) return null;
		else return quests[m_questNumber];
	}

	public bool CurrentMissionIsInProgress()
	{
		return quests[m_questNumber].inProgress;
	}
}
