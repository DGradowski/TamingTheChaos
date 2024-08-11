using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] PlayerMovement m_playerMovement;
    [SerializeField] TamingArea m_taminArea;

    [Header("Audio")]
    [SerializeField] private AudioClip[] m_damageClips;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Vector2 vector = (transform.position - collision.transform.position);
            m_playerMovement.StartHitMotion(vector.normalized);
            SoundFXManager.instance.PlayRandomSoundFXClip(m_damageClips, transform, 1f);
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.GetHit(1, (vector * -1).normalized);
            if (enemy.GetHP() <= 0)
            {
                Destroy(enemy.gameObject);
            }
			CameraControl.instance.ShakeCamera(2f, 0.25f);

			if (m_taminArea.isActiveAndEnabled)
            {
                m_taminArea.FailTaming();
            }
        }
        else if (collision.collider.tag == "Coin")
        {
            Coin coin = collision.collider.gameObject.GetComponent<Coin>();
            SoundFXManager.instance.PlaySoundFXClip(coin.m_clip, transform, 1f);
            Inventory.instance.CreateCoinPopup(coin.gameObject.transform, coin.GetValue());
            Inventory.instance.ChangeSoulsNumber(coin.GetValue());
            Coins.instance.ShowCoins();
            AlertManager.instance.ShowAlerts();
            Destroy(coin.gameObject);
        }
    }
}
