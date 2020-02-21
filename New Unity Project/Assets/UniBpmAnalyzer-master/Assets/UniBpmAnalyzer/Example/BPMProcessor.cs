/*
UniBpmAnalyzer
Copyright (c) 2016 WestHillApps (Hironari Nishioka)
This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using UnityEngine;

public class BPMProcessor : MonoBehaviour
{
    [SerializeField]
    private AudioClip targetClip;
    public int BPM;

    private void Start()
    {
        int bpm = BMPAnalysis.AnalyzeBpm(targetClip);
        if (bpm < 0)
        {
            Debug.LogError("AudioClip is null.");
            return;
        }
        BPM = bpm;
    }
}
