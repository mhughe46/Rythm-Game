using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;
    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (Visualizer._freqBand[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);

        if (Visualizer._freqBand[_band] * _scaleMultiplier >= 10) {
            Debug.Log("spawn");
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z - 3);
            Instantiate(Resources.Load("Sphere"), spawnPos, Quaternion.identity);
        }
    
    }
}
