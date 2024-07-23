using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_blackBackground;
    [SerializeField] GameObject m_text1;
    [SerializeField] GameObject m_text2;
    [SerializeField] GameObject m_continueButton;

    [SerializeField] GameObject m_howToPlay;

	[SerializeField] float t_blackBackground;
	[SerializeField] float t_text1;
	[SerializeField] float t_text2;
	[SerializeField] float t_continueButton;

    bool m_animationIsPlaying = false;
    float m_time = 0f;

	// Update is called once per frame
	void Update()
    {
        if (m_animationIsPlaying)
        {
            m_time += Time.deltaTime;
            if (m_time > t_blackBackground) m_blackBackground.SetActive(true);
			if (m_time > t_text1) m_text1.SetActive(true);
			if (m_time > t_text2) m_text2.SetActive(true);
            if (m_time > t_continueButton)
            {
                m_continueButton.SetActive(true);
                m_animationIsPlaying = false;
            }

		}
    }

    [ContextMenu("Play")]
    public void PlayAnimation()
    {
        m_animationIsPlaying = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseHowToPlay()
    {
        m_howToPlay.SetActive(false);
    }

    public void OpenHowToPlay()
    {
		m_howToPlay.SetActive(true);
	}
}
