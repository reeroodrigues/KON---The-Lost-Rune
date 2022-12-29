using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;

    public float shakeTime;

    [Header("ShakeValue")]
    public float frequency = 3f;
    public float amplitude = 3f;
    public float time = .2f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(frequency, amplitude, time);
    }
    public void Shake(float amplitutde, float frequency, float time)
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitutde;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        }
    }
}
