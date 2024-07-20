using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandle : MonoBehaviour
{
    [SerializeField] Slider m_slider;
    [SerializeField] Image m_image;

    [SerializeField] Sprite[] m_sprites;
    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        UpdateImage();
    }

    public void UpdateImage()
    {
        if (m_slider.value < 0.33) m_image.sprite = m_sprites[0];
        else if (m_slider.value < 0.66) m_image.sprite = m_sprites[1];
        else m_image.sprite = m_sprites[2];
    }
}
