using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public float clipDuration;

    private void OnEnable()
    {
        Invoke(nameof(DisableThis), clipDuration);
    }
    private void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
