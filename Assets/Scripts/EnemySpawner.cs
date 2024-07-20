using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_spawners;
    private Transform[] m_spawnPoints;
    [SerializeField] private float m_spawnSpeed;
    [SerializeField] private GameObject m_sinnerPrefab;
    [SerializeField] private Transform m_playerTransform;
    private float m_spawnTimer = 100;

    private void Start()
    {
        m_spawnPoints = m_spawners.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_spawnTimer += Time.deltaTime;
        if (m_spawnTimer >= m_spawnSpeed)
        {
            SinType type = (SinType)Random.Range(0, 7);
            Vector3 position = m_spawnPoints[Random.Range(1, m_spawnPoints.Length)].position;
            GameObject enemy = Instantiate(m_sinnerPrefab, position, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetTarget(m_playerTransform);
            enemy.GetComponent<Enemy>().SetSinType(type);
            enemy.GetComponent<Enemy>().StartSpawningAnimation();
            m_spawnTimer = 0;
        }
    }
}
