using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillEnemyEventHandle : MonoBehaviour
{
    public ParticleSystem particles;
    public AudioSource audioSource;
    public void DrillStart()
    {
        particles.Play();
    }

    public void DrillStop()
    {
        particles.Stop();
        audioSource.Stop();
    }

    public void AudioStart()
    {
        audioSource.Play();
    }

}
