using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSound : MonoBehaviour
{
    public string Sound;
    public bool isSpecial;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySound(Sound, isSpecial);
    }
}
