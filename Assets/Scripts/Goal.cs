using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
	public static Goal Instance;
	[SerializeField] Image m_goalImage;
	[SerializeField] TMPro.TextMeshProUGUI m_goalText;
	[SerializeField] TMPro.TextMeshProUGUI m_currentText;
	Animator m_animator;

	[SerializeField] Sprite[] m_sinnerSprites;

	float m_timer = 30f;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (m_timer > 1f)
		{
			HideGoal();
		}
		else
		{
			m_timer += Time.unscaledDeltaTime;
		}
	}

	public void UpdateGoal(SinType type)
	{
		if (QuestManager.instance.m_completedQuests < 7)
		{
			int number = 0;

			switch ((int)type)
			{
				case 0:
					number = 0;
					break;
				case 1:
					number = 4;
					break;
				case 2:
					number = 3;
					break;
				case 3:
					number = 2;
					break;
				case 4:
					number = 6;
					break;
				case 5:
					number = 5;
					break;
				case 6:
					number = 1;
					break;
			}

			Quest quest = QuestManager.instance.quests[number];

			m_goalImage.sprite = m_sinnerSprites[(int)type];
			m_goalText.text = quest.requestedQuantity[0].ToString();

			if (quest.inProgress == false)
			{
				m_animator.SetBool("Show", false);
				QuestPopup.instance.HideQuest();
			}
			else
			{
				m_currentText.text = Mathf.Clamp(Inventory.instance.GetSinnersNumber(type), 0, quest.requestedQuantity[0]).ToString();
				if (Inventory.instance.GetSinnersNumber(type) >= quest.requestedQuantity[0])
				{
					QuestPopup.instance.ShowQuest(quest.sprite);
				}
			}
		}

		else
		{
			int number = 7;

			Quest quest = QuestManager.instance.quests[number];

			m_goalImage.sprite = m_sinnerSprites[7];
			m_currentText.text = Mathf.Clamp(Inventory.instance.GetSinnersNumber(type), 0, quest.requestedQuantity[0]).ToString();
			if (Inventory.instance.GetSinnersNumber(type) >= quest.requestedQuantity[0])
			{
				QuestPopup.instance.ShowQuest(quest.sprite);
			}


			int quantity = Inventory.instance.GetSinnersNumber(0);
			quantity += Inventory.instance.GetSinnersNumber((SinType)1);
			quantity += Inventory.instance.GetSinnersNumber((SinType)2);
			quantity += Inventory.instance.GetSinnersNumber((SinType)3);
			quantity += Inventory.instance.GetSinnersNumber((SinType)4);
			quantity += Inventory.instance.GetSinnersNumber((SinType)5);
			quantity += Inventory.instance.GetSinnersNumber((SinType)6);

			m_currentText.text = quantity.ToString();
		}
	}

	public void ShowGoal(SinType type)
	{
		if (QuestManager.instance.m_completedQuests >= 8) return;
		m_timer = 0;
		m_animator.SetBool("Show", true);
		UpdateGoal(type);
	}

	public void HideGoal()
	{
		m_animator.SetBool("Show", false);
	}
}
