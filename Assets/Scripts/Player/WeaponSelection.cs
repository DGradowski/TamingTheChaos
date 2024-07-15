using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSelection : MonoBehaviour
{
	private Vector3 m_mousePos;
	private Vector3 m_mouseWorldPos;
	private Camera m_camera;
	PlayerInputs m_playerInputs;

	private Weapon m_selectedWeapon;
	[SerializeField] private Weapon[] m_weapons;

	private bool m_isActive = true;

	void Start()
	{
		m_camera = Camera.main;
		m_playerInputs = new PlayerInputs();

		m_playerInputs.Movement.Enable();
		m_playerInputs.Movement.FirstWeapon.started += context => SelectWeapon(0);
		m_playerInputs.Movement.SecondWeapon.started += context => SelectWeapon(1);
        m_playerInputs.Movement.Attack.started += UseWeapon;
        m_playerInputs.Movement.Attack.canceled += DisableWeapon;
        SelectWeapon(0);
	}

    private void UseWeapon(InputAction.CallbackContext context)
    {
		m_selectedWeapon.Attack();
    }

	private void DisableWeapon(InputAction.CallbackContext context)
	{
		m_selectedWeapon.StopAttack();
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

	void SelectWeapon(int index)
	{
		if (!m_isActive) return;
		if (index >= m_weapons.Length) return;

		foreach (Weapon weapon in m_weapons)
		{
			weapon.gameObject.SetActive(false);
		}
		m_weapons[index].gameObject.SetActive(true);
		m_selectedWeapon = m_weapons[index];
		m_selectedWeapon.gameObject.transform.position = transform.position;
	}

	public void DisableSelectingWeapon()
	{
		m_isActive = false;
	}

    public void EnableSelectingWeapon()
    {
        m_isActive = true;
    }
}
