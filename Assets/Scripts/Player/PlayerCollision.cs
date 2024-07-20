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
            if (m_taminArea.isActiveAndEnabled)
            {
                m_taminArea.FailTaming();
            }
        }
        else if (collision.collider.tag == "Coin")
        {
            Coin coin = collision.collider.gameObject.GetComponent<Coin>();
            SoundFXManager.instance.PlaySoundFXClip(coin.m_clip, transform, 1f);
            FindAnyObjectByType<Inventory>().ChangeSoulsNumber(coin.GetValue());
            Destroy(coin.gameObject);
        }
    }
}
