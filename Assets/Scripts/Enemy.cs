using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SinName
{
	Gluttony,
	Pride,
	Lust,
	Anger,
	Greed
}

public class Enemy : MonoBehaviour
{
	[Header("Enemy Stats")]
	[SerializeField] private float m_damage;
	[SerializeField] private float m_speed;
	[SerializeField] private float hitTime;
	[SerializeField] private float hitSpeed;
	[SerializeField] private Transform m_playerTransform;
	[SerializeField] private EnemyHealth m_healthSprite;
	
	private Animator m_animator;

	private int m_currentHP;
	private bool m_inHitMotion;
	private Vector2 m_moveVector;
	private float m_hitTimer;
	

	// Start is called before the first frame update
	void Start()
	{
		m_currentHP = 4;
		m_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (m_inHitMotion) m_hitTimer -= Time.deltaTime;
		if (m_hitTimer <= 0 && m_inHitMotion) StopHitMotion();
	}

	private void FixedUpdate()
	{
		if (m_inHitMotion)
		{
			transform.position += new Vector3(m_moveVector.x, m_moveVector.y, 0) * (hitSpeed * (m_hitTimer / hitTime));
		}
		else
		{
			m_moveVector = m_playerTransform.position - transform.position;
			transform.position += new Vector3(m_moveVector.x, m_moveVector.y, 0).normalized * m_speed;
			m_animator.SetBool("Walking", true);
		}
	}

	public float GetDamage() { return m_damage; }

	public void GetHit(int damage, Vector2 moveVector)
	{
		StartHitMotion(moveVector);
		m_currentHP -= damage;
		m_healthSprite.UpdateHealth(m_currentHP - 1);
	}

	public void StartHitMotion(Vector2 moveVector)
	{
		m_inHitMotion = true;
		m_moveVector = moveVector;
		m_hitTimer = hitTime;
		m_animator.SetBool("HitMotion", true);
		m_animator.SetFloat("xVector", moveVector.x);
	}

	public void StopHitMotion()
	{
		m_inHitMotion = false;
		m_moveVector = Vector2.zero;
		m_animator.SetBool("HitMotion", false);
	}
}
