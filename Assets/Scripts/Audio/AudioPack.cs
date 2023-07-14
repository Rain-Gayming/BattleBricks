using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio Pack")]
public class AudioPack : ScriptableObject
{
    public List<SoundEffect> clips;
}
