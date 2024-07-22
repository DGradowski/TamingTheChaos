using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using UnityEngine.UI;

public class TamingArea : MonoBehaviour
{
	[SerializeField] private Hands m_hands;
	[SerializeField] private ProgressImage[] m_progressImages;
	[SerializeField] private GameObject m_goodPopParticle;
	private RectTransform m_rectTransform;
	private float m_timer;
	[SerializeField] private float m_tamingTime;
	[SerializeField] private int m_stages;
	private int m_currentStage = 0;

	private int m_eyesToClick = 0;
	private int m_extraEyes = 0;
	private int m_clickedEyes = 0;

	[Header("Game Objects")]
	[SerializeField] GameObject m_eye;
	[SerializeField] Inventory m_inventory;
	[SerializeField] ComboText m_comboText;

	[Header("Audio")]
	[SerializeField] AudioClip[] m_tamingClips;
	[SerializeField] AudioClip[] m_successClips;
	[SerializeField] AudioClip[] m_failureClips;

	private float m_min_width;
	private float m_max_width;
	private float m_min_height;
	private float m_max_height;

	// Update is called once per frame
	void Update()
	{
		m_timer += Time.deltaTime;
		if (m_tamingTime / m_stages <= m_timer)
		{
			m_currentStage++;
			m_eye.GetComponent<TamingPoint>().ChangeStage(m_currentStage);
			m_timer = 0;
			if (m_currentStage > m_stages)
			{
				if (m_clickedEyes < m_eyesToClick) FailTaming();
				else SuccessTaming();
			}
        }
    }

	public void StartTaming(int min_eyes, int extra, float taming_time)
	{
		m_clickedEyes = -1;
		m_eyesToClick = min_eyes;
		m_extraEyes = extra;
		m_tamingTime = taming_time;
		ShowNewEye(true);
		ActivateAllImages();
	}

	public void ShowNewEye(bool first)
	{
		m_eye.GetComponent<TamingPoint>().ChangeStage(0);
		if (!first) Instantiate(m_goodPopParticle, Camera.main.ScreenToWorldPoint(m_eye.transform.position), m_eye.transform.rotation);
		GenerateNewPosition();
        m_timer = 0;
		m_currentStage = 0;
		m_clickedEyes++;
		CheckSuccessed(m_clickedEyes - 1);
		m_comboText.UpdateText(m_clickedEyes - m_eyesToClick);
		if (m_clickedEyes <= m_eyesToClick) SoundFXManager.instance.PlaySoundFXClip(m_tamingClips[0], transform, 1f);
		else SoundFXManager.instance.PlaySoundFXClip(m_tamingClips[1], transform, 1f);
		if (m_clickedEyes >= m_eyesToClick + m_extraEyes)
        {
			SuccessTaming();
        }
    }

	public void GenerateNewPosition()
	{
        m_rectTransform = GetComponent<RectTransform>();

        float eye_width = m_eye.GetComponent<RectTransform>().rect.width;
        float eye_height = m_eye.GetComponent<RectTransform>().rect.height;

        m_max_width = m_rectTransform.rect.width - eye_width;
        m_max_height = m_rectTransform.rect.height - eye_height - 200;

        m_min_width = 0 + eye_width;
        m_min_height = 0 + eye_height;

        float x_pos = Random.value * (m_max_width - m_min_width) + m_min_width;
        float y_pos = Random.value * (m_max_height - m_min_height) + m_min_height;

        m_eye.transform.position = new Vector3(x_pos, y_pos, 0);
    }

	public void StopTaming()
	{
		m_hands.ReleaseEnemy();
		m_hands.EnableWeapon();
		gameObject.SetActive(false);
		DeactivateAllImages();
		m_comboText.UpdateText(0);
	}

	public void ActivateAllImages()
	{
		foreach(ProgressImage image in m_progressImages)
		{
			image.Activate();
		}
	}

    public void DeactivateAllImages()
    {
        foreach (ProgressImage image in m_progressImages)
        {
            image.Deactivate();
        }
    }

	public void SuccessTaming()
	{
		int combo = m_clickedEyes - m_eyesToClick + 1;
		int basic = 1 + UpgradesManager.instance.basicValue;
		int value = CalculateValue(basic, m_hands.GetChatchedEnemyHP(), combo);
		m_inventory.ChangeSinnersNumber(m_hands.GetChatchedEnemyType(), value);
		SoundFXManager.instance.PlayRandomSoundFXClip(m_successClips, transform, 1f);
		StopTaming();
	}

	public void FailTaming()
	{
        foreach (ProgressImage image in m_progressImages)
        {
            image.SetFail();
        }
        SoundFXManager.instance.PlayRandomSoundFXClip(m_failureClips, transform, 1f);
        StopTaming();
    }

	public void CheckSuccessed(int value)
	{
		if (value < 0 || value >= m_progressImages.Length) return;
		m_progressImages[value].SetSuccess();
	}

    public int CalculateValue(int basic, int enemyHP, int combo)
    {
        if (combo <= 0) combo = 1;
        return (basic + (enemyHP * (UpgradesManager.instance.healthBonus + 1))) * combo;
    }

}
