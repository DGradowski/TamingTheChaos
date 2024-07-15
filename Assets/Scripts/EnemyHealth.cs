using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Sprite[] m_sprites;
    SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealth(4);
    }

    public void UpdateHealth(int value)
    {

        if (value > 2 || value < 0) m_spriteRenderer.sprite = null;
        else m_spriteRenderer.sprite = m_sprites[value];
    }
}
