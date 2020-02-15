using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDelayer : MonoBehaviour
{
    private void Start()
    {
        Invoke("StartMusic", 1.35f);
    }

    void StartMusic() {
        GetComponent<AudioSource>().Play();
    }

}
