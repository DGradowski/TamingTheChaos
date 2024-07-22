using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteButton : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI completeText;
	[SerializeField] private TMPro.TextMeshProUGUI completeShadow;
	[SerializeField] private string buttonText;
	private Animator completeTextAnimator;

    private Button m_completeButton;

	private void Awake()
	{
		m_completeButton = GetComponent<Button>();
		completeTextAnimator = completeText.GetComponent<Animator>();
	}

	[ContextMenu("Enable")]
	public void Enable()
	{
		m_completeButton.interactable = true;
		completeText.text = buttonText;
		completeShadow.text = buttonText;
	}

    [ContextMenu("Disable")]
    public void Disable()
    {
		completeText.text = "";
		completeShadow.text = "";
		m_completeButton.interactable = false;
    }

    public void OnPress()
	{
		if (m_completeButton.interactable == false) return;
        completeTextAnimator.SetBool("Press", true);
		SoundFXManager.instance.PlayMenuSound(transform, 1f);
	}

	public void OnRelease()
	{
		if (m_completeButton.interactable == false) return;
		completeTextAnimator.SetBool("Press", false);
	}
}
