using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected Animator m_animator;
	public abstract void Attack();
}
