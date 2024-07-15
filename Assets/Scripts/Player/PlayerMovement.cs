using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement Values")]
	public float defaultSpeed;
	public float runSpeed;
    public float hitTime;
	public float hitSpeed;

    [Header("Player Game Objects")]
	[SerializeField] private Animator m_animator;
	[SerializeField] private GameObject m_sprite;

	bool m_inHitMotion;
    float m_hitTimer;

	private Transform m_transform;
	private float m_movementSpeed;
	private Vector2 m_moveVector;

	PlayerInputs m_playerInputs;

    void Start()
	{
		m_transform = GetComponent<Transform>();
		m_moveVector = Vector2.zero;
		m_movementSpeed = defaultSpeed;

		m_playerInputs = new PlayerInputs();
		m_inHitMotion = false;

		m_playerInputs.Movement.Enable();
		m_playerInputs.Movement.Move.performed += Move;
		m_playerInputs.Movement.Move.canceled += CancelMove;

        m_playerInputs.Movement.Run.started += StartRun;
        m_playerInputs.Movement.Run.canceled += CancelRun;

    }
    void FixedUpdate()
	{
		if (!m_inHitMotion) m_transform.position += new Vector3(m_moveVector.x, m_moveVector.y, 0) * m_movementSpeed;
		else m_transform.position += new Vector3(m_moveVector.x, m_moveVector.y, 0) * (hitSpeed * (m_hitTimer / hitTime));
    }

    void Update()
    {
        if (m_inHitMotion) m_hitTimer -= Time.deltaTime;
		if (m_hitTimer <= 0 && m_inHitMotion) StopHitMotion();
    }

    public void Move(InputAction.CallbackContext context)
	{
		if (m_inHitMotion) return;
		m_moveVector = context.ReadValue<Vector2>();
        m_animator.SetBool("Walking", true);
	}

	public void CancelMove(InputAction.CallbackContext context)
	{
        if (m_inHitMotion) return;
        m_moveVector = Vector2.zero;
		m_animator.SetBool("Walking", false);
	}

    public void StartRun(InputAction.CallbackContext context)
    {
        m_moveVector = m_moveVector.normalized;
		m_movementSpeed = runSpeed;
    }
	public void CancelRun(InputAction.CallbackContext context) => m_movementSpeed = defaultSpeed;

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
        m_moveVector = m_playerInputs.Movement.Move.ReadValue<Vector2>();
		m_animator.SetBool("HitMotion", false);
        if (m_moveVector == Vector2.zero) m_animator.SetBool("Walking", false);
    }
}
