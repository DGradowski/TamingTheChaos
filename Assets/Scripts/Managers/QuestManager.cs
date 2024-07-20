using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	[SerializeField] public Quest[] quests;
	[SerializeField] public int m_questNumber = 0;

	public void ChangeQuestNuber(int i)
	{
		m_questNumber += i;
		if (m_questNumber < 0) m_questNumber = quests.Length - 1;
		if (m_questNumber >= quests.Length) m_questNumber = 0;
		SoundFXManager.instance.PlayMenuSound(FindAnyObjectByType<PlayerMovement>().transform, 1f);
		Menu.instance.UpdateMenuValues();
	}

	public void CompleteQuest()
	{
		if (quests[m_questNumber].inProgress)
		{
			if (IsRequestCompleted())
			{
				for (int i = 0; i < quests[m_questNumber].requestedType.Length; i++)
				{
					SinType requestedType = quests[m_questNumber].requestedType[i];
					int requestedQuantity = quests[m_questNumber].requestedQuantity[i];
					Inventory.instance.ChangeSinnersNumber(requestedType, requestedQuantity * -1);
				}
				Inventory.instance.ChangeSoulsNumber(quests[m_questNumber].reward);
				quests[m_questNumber].inProgress = false;
			}
		}
		Menu.instance.UpdateMenuValues();
	}

	public bool IsRequestCompleted()
	{
		for (int i = 0; i < quests[m_questNumber].requestedType.Length; i++)
		{
			SinType requestedType = quests[m_questNumber].requestedType[i];
			int requestedQuantity = quests[m_questNumber].requestedQuantity[i];

			if (Inventory.instance.GetSinnersNumber(requestedType) < requestedQuantity) return false;
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
