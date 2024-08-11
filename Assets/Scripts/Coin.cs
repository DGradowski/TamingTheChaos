using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float m_baseSpeed;
    [SerializeField] int m_baseValue;
    [SerializeField] float m_changeTime;
    [SerializeField] float m_magneticDrag;

    float m_speed;
    int m_value;
    int m_stage = 2;

    [Header("Other")]
    [SerializeField] Sprite[] m_sprites;
    [SerializeField] public AudioClip m_clip;

    [SerializeField] private SpriteRenderer m_spriteRenderer;
    private Transform m_playerTransform;
    private float m_timer = 0;
    private bool m_inMagnetRange = false;

    private Vector2 moveVector;
	Vector2 velocity = Vector2.zero;

	// Start is called before the first frame update
	void Start()
    {
        m_playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
        m_value = m_baseValue;
        m_spriteRenderer.sprite = m_sprites[m_stage];
        m_value = 8;
        moveVector = transform.position - m_playerTransform.position;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newVector = transform.position - m_playerTransform.position;
        m_speed = (m_baseSpeed / 3) * (m_stage + 1);
        Vector2 magnetVector = Vector2.zero;

        

        if (m_inMagnetRange)
        {
			magnetVector = m_playerTransform.position - transform.position;
            float distance = Vector2.Distance(m_playerTransform.position, transform.position);
            magnetVector = magnetVector.normalized * (m_magneticDrag / (distance * distance + 2)) * UpgradesManager.instance.soulsMagnet * 1.5f;
		}

        moveVector = Vector2.SmoothDamp(moveVector, newVector, ref velocity, .3f).normalized * m_speed + magnetVector;

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

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Magnet")
        {
            m_inMagnetRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Magnet")
		{
            m_inMagnetRange = false;
		}
	}
}
