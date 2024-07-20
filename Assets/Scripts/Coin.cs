using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float m_baseSpeed;
    [SerializeField] int m_baseValue;
    [SerializeField] float m_changeTime;
    float m_speed;
    int m_value;
    int m_stage = 2;

    [Header("Other")]
    [SerializeField] Sprite[] m_sprites;
    [SerializeField] public AudioClip m_clip;

    private SpriteRenderer m_spriteRenderer;
    private Transform m_playerTransform;
    private float m_timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
        m_value = m_baseValue;
        m_spriteRenderer.sprite = m_sprites[m_stage];
        m_value = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector = transform.position - m_playerTransform.position;
        m_speed = (m_baseSpeed / 3) * (m_stage + 1);

        moveVector = moveVector.normalized * m_speed;

        transform.position += new Vector3(moveVector.x, moveVector.y, 0);
    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= m_changeTime)
        {
            m_stage--;
            if (m_stage < 0)
            {
                Destroy(gameObject);
                return;
            }
            m_value = m_baseValue + m_stage * 3;
            m_spriteRenderer.sprite = m_sprites[m_stage];
            m_timer = 0;
        }
    }

    public int GetValue()
    {
        return m_value;
    }
}
