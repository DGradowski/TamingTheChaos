using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	private enum Panel
	{
		Quest,
		Shop,
		HowToPlay
	}

	public bool menuIsActive;
	private Panel m_currentPanel = Panel.Quest;

	[Header("GameObjects")]
	[SerializeField] private GameObject m_menuPanel;
	[SerializeField] private Inventory m_inventory;
	// Start is called before the first frame update
	private PlayerInputs m_playerInputs;

	[Header("Inventory Panel")]
	[SerializeField] TMPro.TextMeshProUGUI m_angerText;
	[SerializeField] TMPro.TextMeshProUGUI m_gluttonyText;
	[SerializeField] TMPro.TextMeshProUGUI m_greedText;
	[SerializeField] TMPro.TextMeshProUGUI m_lustText;
	[SerializeField] TMPro.TextMeshProUGUI m_prideText;
	[SerializeField] TMPro.TextMeshProUGUI m_envyText;
	[SerializeField] TMPro.TextMeshProUGUI m_slothText;

	[SerializeField] TMPro.TextMeshProUGUI m_soulsText;

	[Header("Quest Panel")]
	[SerializeField] GameObject m_questPanel;
	[SerializeField] QuestManager m_questManager;
	[SerializeField] Image m_devilImage;
	[SerializeField] TMPro.TextMeshProUGUI m_devilName;
	[SerializeField] TMPro.TextMeshProUGUI m_devilSobriquet;
	[SerializeField] TMPro.TextMeshProUGUI m_questText;
	[SerializeField] GameObject m_requestCointainer;
	[SerializeField] GameObject m_requestPrefab;
	[SerializeField] TMPro.TextMeshProUGUI m_reward;
	[SerializeField] GameObject m_rewardContainer;
	[SerializeField] CompleteButton m_completeButton;
	[SerializeField] GameObject m_completedText;

	[Header("Shop panel")]
	[SerializeField] GameObject m_shopPanel;
	[SerializeField] GameObject m_upgradeContainer;
	[SerializeField] TMPro.TextMeshProUGUI m_upgradeCost;
	[SerializeField] TMPro.TextMeshProUGUI m_upgradeName;
	[SerializeField] TMPro.TextMeshProUGUI m_upgradeDescription;
	[SerializeField] Image[] m_progressImages;
	[SerializeField] Sprite m_filledSprite;
	[SerializeField] Sprite m_emptySprite;
	[SerializeField] CompleteButton m_buyButton;

	[Header("How To Play Panel")]
	[SerializeField] GameObject m_howToPlayPanel;
	[SerializeField] GameObject[] m_htpPages;
	int m_pageNumber = 0;

	public static Menu instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		m_playerInputs = new PlayerInputs();

		m_playerInputs.Movement.Enable();
		m_playerInputs.Movement.Menu.performed += MenuButton;
	}

	private void MenuButton(InputAction.CallbackContext context)
	{
		TurnOnOffMenu();
	}

	public void TurnOnOffMenu()
	{
		if (m_menuPanel == null) return;
		menuIsActive = !menuIsActive;
		m_menuPanel.SetActive(menuIsActive);
		SoundFXManager.instance.PlayMenuSound(transform, .5f);
		UpdateMenuValues();
		if (menuIsActive)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void UpdateMenuValues()
	{
		m_angerText.text = m_inventory.angerNumber.ToString();
		m_lustText.text = m_inventory.lustNumber.ToString();
		m_prideText.text = m_inventory.prideNumber.ToString();
		m_greedText.text = m_inventory.greedNumber.ToString();
		m_gluttonyText.text = m_inventory.gluttonyNumber.ToString();
		m_envyText.text = m_inventory.envyNumber.ToString();
		m_slothText.text = m_inventory.slothNumber.ToString();
		m_soulsText.text = m_inventory.soulsNumber.ToString();
		AlertManager.instance.ShowAlerts();

		if (m_currentPanel == Panel.Quest)
		{
			UpdateQuestPanel();
		}
		else if (m_currentPanel == Panel.Shop)
		{
			UpdateShopPanel();
		}
		else
		{
			UpdateHowToPlayPanel();
		}
	}

	public void UpdateQuestPanel()
	{
		m_questPanel.SetActive(true);
		m_shopPanel.SetActive(false);
		m_howToPlayPanel.SetActive(false);
		Quest quest = m_questManager.GetCurrentQuest();
		if (quest == null) return;
		m_devilImage.sprite = quest.sprite;
		m_devilName.text = quest.name;
		m_devilSobriquet.text = quest.sobriquet;
		if (m_questManager.CurrentMissionIsInProgress())
		{
			m_completedText.SetActive(false);
			m_rewardContainer.SetActive(true);
			m_reward.text = quest.reward.ToString();
			m_questText.text = quest.inProgressText;
			
			for (var i = m_requestCointainer.transform.childCount - 1; i >= 0; i--)
			{
				Destroy(m_requestCointainer.transform.GetChild(i).gameObject);
			}

			for (int i = 0; i < quest.requestedType.Length; i++)
			{
				var request = Instantiate(m_requestPrefab, m_requestCointainer.transform).GetComponent<Request>();
				request.SetRequest(quest.requestedType[i], quest.requestedQuantity[i]);
			}
			if (m_questManager.IsRequestCompleted(m_questManager.m_questNumber)) m_completeButton.Enable();
			else m_completeButton.Disable();
		}
		else
		{
			m_rewardContainer.SetActive(false);
			m_questText.text = quest.completedText;
			for (var i = m_requestCointainer.transform.childCount - 1; i >= 0; i--)
			{
				Destroy(m_requestCointainer.transform.GetChild(i).gameObject);
				m_completeButton.Enable();
			}
			m_completedText.SetActive(true);
			m_completeButton.Disable();
		}
	}

	public void UpdateShopPanel()
	{
		m_questPanel.SetActive(false);
		m_shopPanel.SetActive(true);
		m_howToPlayPanel.SetActive(false);
		UpgradesManager upgrades = UpgradesManager.instance;
		if (upgrades.GetProgress() >= 3) m_upgradeContainer.SetActive(false);
		else
		{
			m_upgradeContainer.SetActive(true);
			m_upgradeCost.text = upgrades.GetCost().ToString();
		}
		m_upgradeName.text = upgrades.GetName();
		m_upgradeDescription.text = upgrades.GetDescription();
		for (int i = 0; i < 3; i++)
		{
			m_progressImages[i].sprite = m_emptySprite;
		}
		for (int i = 0; i < upgrades.GetProgress(); i++)
		{
			m_progressImages[i].sprite = m_filledSprite;
		}
		if (upgrades.CanBuyUpgrade()) m_buyButton.Enable();
		else m_buyButton.Disable();
	}

	public void UpdateHowToPlayPanel()
	{
		m_questPanel.SetActive(false);
		m_shopPanel.SetActive(false);
		m_howToPlayPanel.SetActive(true);
		foreach(GameObject x in m_htpPages)
		{
			x.SetActive(false);
		}
		m_htpPages[m_pageNumber].SetActive(true);
	}

	public void ChangeHowToPlayPage(int value)
	{
		m_pageNumber += value;
		if (m_pageNumber < 0) m_pageNumber = m_htpPages.Length - 1;
		else if (m_pageNumber >= m_htpPages.Length) m_pageNumber = 0;
		SoundFXManager.instance.PlayMenuSound(transform, 1f);
		UpdateHowToPlayPanel();
	}

	public void OpenQuests()
	{
		m_currentPanel = Panel.Quest;
		UpdateMenuValues();
		SoundFXManager.instance.PlayMenuSound(transform, 1f);
	}

	public void OpenShop()
	{
		m_currentPanel = Panel.Shop;
		UpdateMenuValues();
		SoundFXManager.instance.PlayMenuSound(transform, 1f);
	}

	public void OpenHowToPlay()
	{
		m_currentPanel = Panel.HowToPlay;
		UpdateMenuValues();
		SoundFXManager.instance.PlayMenuSound(transform, 1f);
	}
}
