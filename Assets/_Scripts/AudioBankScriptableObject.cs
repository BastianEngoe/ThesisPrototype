using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioBank", order = 1)]
public class AudioBankScriptableObject : ScriptableObject
{
    public List<AudioClip> audioFiles;
}
