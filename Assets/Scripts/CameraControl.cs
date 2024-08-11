using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
	private CinemachineVirtualCamera m_cinemachineVC;
	private float shakeTimer;
	private float m_shakeTimerTotal;
	private float m_startingIntensity;

	public static CameraControl instance;

	private void Awake()
	{
		instance = this;
		m_cinemachineVC = GetComponent<CinemachineVirtualCamera>();
	}

	public void ShakeCamera(float intensity, float time)
	{
		CinemachineBasicMultiChannelPerlin channelPerlin = m_cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		channelPerlin.m_AmplitudeGain = intensity;
		shakeTimer = time;
		m_shakeTimerTotal = time;
		m_startingIntensity = intensity;
	}

	private void Update()
	{
		if (shakeTimer < 0) return;
		shakeTimer -= Time.deltaTime;
		CinemachineBasicMultiChannelPerlin channelPerlin = m_cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		channelPerlin.m_AmplitudeGain = Mathf.Lerp(m_startingIntensity, 0f, 1 - (shakeTimer / m_shakeTimerTotal));
	}
}
