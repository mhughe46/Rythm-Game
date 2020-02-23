using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleProp : MonoBehaviour
{
    [SerializeField]
    SpawnNotes _noteSpawner;

    public float _scaleMultiplier = 1;
    private Vector3 _scale;
    // Start is called before the first frame update
    void Start()
    {
        _scale = this.transform.localScale;
        _noteSpawner = FindObjectOfType<SpawnNotes>();
    }

    // Update is called once per frame
    void Update()
    {
        Scale();
    }

    private void Scale()
    {
        if (_noteSpawner.spectrumData[0] >=5)
        {
            _scaleMultiplier = 1.5f;
        }else if (_noteSpawner.spectrumData[0] >= 2.5f)
        {
            _scaleMultiplier = 1.25f;

        }else if (_noteSpawner.spectrumData[0] >= 1f)
        {
            _scaleMultiplier = 0.75f;
        }else
        {
            _scaleMultiplier = 1;
        }
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(_scale.x, 1 * _scaleMultiplier, _scale.z), Time.deltaTime * 5);
    }

 
}
