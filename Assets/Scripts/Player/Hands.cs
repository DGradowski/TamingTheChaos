using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Weapon
{
    Enemy m_catchedEnemy;
    [SerializeField] public GameObject m_enemyHolder;
    [SerializeField] private TamingArea m_tamingArea;

    [SerializeField] private float m_baseTamingTime;

    public override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_catchedEnemy != null) return;
        if (collision.tag != "Enemy") return;
        m_catchedEnemy = collision.GetComponent<Enemy>();
        m_catchedEnemy.GetComponent<BoxCollider2D>().enabled = false;
        m_catchedEnemy.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 1);
        m_tamingArea.gameObject.SetActive(true);
        float tamingTime = 0.25f + (0.25f * (4 - m_catchedEnemy.GetHP()) + (0.25f * UpgradesManager.instance.extraTime));
        m_tamingArea.StartTaming(5, UpgradesManager.instance.maxCombo, tamingTime);
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
        m_catchedEnemy.GetComponent<Transform>().position = m_enemyTransform.position;
        m_catchedEnemy.GetComponent<SpriteRenderer>().sortingOrder = 15;
        DisableWeapon();
    }

    public void ReleaseEnemy()
    {
        if (m_catchedEnemy == null) return;
        Destroy(m_catchedEnemy.gameObject);
        m_catchedEnemy = null;
        m_enemyHolder.SetActive(false);
        EnableWeapon();
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
        if (!m_isActive) return;
        m_animator.SetBool("Attack", true);
        m_collider.enabled = true;
    }

    public override void StopAttack()
    {
        m_animator.SetBool("Attack", false);
    }

	void DisableCollision()
	{
		m_collider.enabled = false;
	}

	public SinType GetChatchedEnemyType()
    {
        if (m_catchedEnemy == null) return SinType.None;
        else return m_catchedEnemy.GetSinType();
    }

    public int GetChatchedEnemyHP()
    {
        if (m_catchedEnemy == null) return 0;
        else return m_catchedEnemy.GetHP();
    }
}
