using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSelection : MonoBehaviour
{
    private Vector3 m_mousePos;
    private Vector3 m_mouseWorldPos;
    private Camera m_camera;
    PlayerInputs m_playerInputs;

    [SerializeField] private Weapon[] m_weapons;
    void Start()
    {
        m_camera = Camera.main;
    }

    private void Update()
    {
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        m_mousePos = Mouse.current.position.ReadValue();
        m_mouseWorldPos = m_camera.ScreenToWorldPoint(m_mousePos);
        transform.LookAt(m_mouseWorldPos);
        var direction = (m_mouseWorldPos - transform.position).normalized;
        Vector3 targ = m_mouseWorldPos;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        float angle;
        if (targ.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg + 180;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
