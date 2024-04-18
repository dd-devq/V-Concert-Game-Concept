using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentManager<AudioManager>
{
    [SerializeField]
    private AudioSource _hitSFX = null;
    [SerializeField]
    private AudioSource _missSFX = null;
    public override void Awake()
    {
        base.Awake();
    }
    public void PlayHitSFX()
    {
        _hitSFX.Play();
    }
    public void PlayMissSFX()
    {
        _missSFX.Play();
    }
}
