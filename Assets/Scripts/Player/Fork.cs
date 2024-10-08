using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fork : Weapon
{
    [SerializeField] private float m_animationTime;
    [SerializeField] private int m_damage;
    [SerializeField] private Animator m_motionAnimator;

    [SerializeField] private List<Enemy> m_enemiesInRange;
    [SerializeField] private List<Enemy> m_attackedEnemies;
    [SerializeField] private List<Enemy> m_enemiesToDestroy;
    [SerializeField] private Sprite m_goldenFork;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    void ExecuteAttack()
    {
		foreach (Enemy enemy in m_enemiesInRange)
		{
			bool attacked = false;
			foreach (Enemy attackedEnemy in m_attackedEnemies)
			{
				if (attackedEnemy == enemy)
				{
					attacked = true;
					break;
				}
			}
			if (!attacked)
			{
				Vector2 vector = (enemy.transform.position - transform.position);
				enemy.GetHit(m_damage, vector.normalized);
                CameraControl.instance.ShakeCamera(1.2f, 0.2f);
                if (enemy.GetHP() == 0)
				{
					m_enemiesToDestroy.Add(enemy);
				}
				else m_attackedEnemies.Add(enemy);
			}
		}

		DestroyEnemies();
	}

    void ClearAttackedEnemies()
    {
        m_attackedEnemies.Clear();
    }

    public override void Attack()
    {
        if (!m_isActive) return;
        m_animator.SetBool("Attack", true);
        m_motionAnimator.SetBool("Attack", true);
        m_collider.enabled = true;
    }

    public override void StopAttack()
    {
        m_animator.SetBool("Attack", false);
		m_motionAnimator.SetBool("Attack", false);
	}

	void DisableCollision()
	{
		m_collider.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy") return;
        if (m_enemiesInRange.Contains(collision.GetComponent<Enemy>())) return;
        m_enemiesInRange.Add(collision.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Enemy") return;
        m_enemiesInRange.Remove(collision.GetComponent<Enemy>());
    }

    private void DestroyEnemies()
    {
        for (int i = 0; i < m_enemiesToDestroy.Count;)
        {
            Enemy enemy = m_enemiesToDestroy[i];
            m_attackedEnemies.Remove(enemy);
            m_enemiesInRange.Remove(enemy);
            m_enemiesToDestroy.Remove(enemy);
            Instantiate(enemy.m_coinPrefab, enemy.transform.position, Quaternion.identity);
            Destroy(enemy.gameObject);
        }
    }

    public void UpgradeFork()
    {
        GetComponent<SpriteRenderer>().sprite = m_goldenFork;
    }
}
