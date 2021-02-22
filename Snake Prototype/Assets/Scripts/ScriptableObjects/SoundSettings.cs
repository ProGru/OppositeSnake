using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName ="SoundSettings",menuName ="Sound/SoundSettings")]
public class SoundSettings : ScriptableObject
{
    public float volume;
    public bool isMuted;
    public AudioClip defaultButtonClip;
}
