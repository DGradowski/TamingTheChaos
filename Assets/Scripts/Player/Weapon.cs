using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected Animator m_animator;
    [SerializeField] protected GameObject m_weaponSelector;
    [SerializeField] protected Transform m_enemyTransform;
    [SerializeField] protected BoxCollider2D m_collider;
    protected SpriteRenderer m_spriteRenderer;
    protected bool m_isActive = true;

    public virtual void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_isActive = true;
        m_collider = GetComponent<BoxCollider2D>();
        m_collider.enabled = false;
    }

    public abstract void Attack();

    public abstract void StopAttack();
}
