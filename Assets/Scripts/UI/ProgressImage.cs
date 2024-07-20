using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressImage : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Sprite m_noneSprite;
    [SerializeField] Sprite m_successSprite;
    [SerializeField] Sprite m_failSprite;

    private Image m_image;
    private Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        m_animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        m_image.sprite = m_noneSprite;
        m_animator.SetBool("IsActive", true);
    }

    public void Deactivate()
    {
        m_animator.SetBool("IsActive", false);
    }

    public void SetNone()
    {
        m_image.sprite = m_noneSprite;
    }

    public void SetSuccess()
    {
        m_image.sprite = m_successSprite;
        m_animator.SetTrigger("Shake");
    }

    public void SetFail()
    {
        m_image.sprite = m_failSprite;
        m_animator.SetTrigger("Shake");
    }
}
