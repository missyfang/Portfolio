using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    public static void PlayAudio(string clip)
    {
        AudioSource.PlayClipAtPoint(Resources.Load(clip) as AudioClip, Camera.main.transform.position);
    }
}
