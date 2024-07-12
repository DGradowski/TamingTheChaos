using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Weapon
{
    Enemy m_catchedEnemy;
    [SerializeField] private Transform m_enemyPosition;
    [SerializeField] private GameObject m_enemyHolder;
    [SerializeField] private GameObject m_weaponSelector;

    private SpriteRenderer m_spriteRenderer;
    private bool m_isActive = true;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_catchedEnemy != null) return;
        if (collision.tag != "Enemy") return;
        m_catchedEnemy = collision.GetComponent<Enemy>();
        m_catchedEnemy.GetComponent<BoxCollider2D>().enabled = false;
        m_catchedEnemy.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        HoldEnemy();
    }

    private void HoldEnemy()
    {
        if (m_catchedEnemy == null) return;
        m_enemyHolder.SetActive(true);
        m_catchedEnemy.GetComponent<Transform>().position = m_enemyPosition.position;
        DisableWeapon();
    }

    public void DisableWeapon()
    {
        m_spriteRenderer.enabled = false;
        m_weaponSelector.GetComponent<WeaponSelection>().DisableSelectingWeapon();
        m_isActive = false;
    }

    public void EnableWeapon()
    {
        m_spriteRenderer.enabled = true;
        m_weaponSelector.GetComponent<WeaponSelection>().EnableSelectingWeapon();
        m_isActive = true;
    }

    public override void Attack()
    {
        m_animator.SetTrigger("Attack");
    }
}
