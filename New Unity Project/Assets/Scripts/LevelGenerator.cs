using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    List<Transform> _pathsEmpty;
    [SerializeField]
    List<Transform> _pathsObstacles;
    private bool isGenerating;

    [SerializeField]
    List<Transform> _activePaths;

    [SerializeField]
    Vector3 _pathSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GenerateCircle();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPathForGarabge();
        
    }

    private void GenerateCircle()
    {
        for(int i = 360; i > 0; i -= 15)
        {
            CreatePath(0, i);
        }
    }
    private void CheckPathForGarabge()
    {
        if (_activePaths[0].eulerAngles.x >= 320f && _activePaths[0].eulerAngles.x <= 321f)
        {
            RecyclePath();
        }
        if (_activePaths[_activePaths.Count - 1].eulerAngles.x <= 75f)
        {
            CreatePath(0);
        }
    }
    private void RecyclePath(int index = 0)
    {
        Destroy(_activePaths[index].gameObject);
        _activePaths.RemoveAt(0);
    }
    private void CreatePath(int index, float rotation = 90)
    {
        Transform newPath = Instantiate(_pathsEmpty[Random.Range(0,3)], this.transform.position + _pathSpawnPoint, Quaternion.Euler(rotation, 0, 0), this.transform);
        newPath.GetComponent<RotatePath>()._pivot = this.transform;
        _activePaths.Add(newPath);
    }

    //called 1.35 seconds before the note should be hit
    public void noteSpawned(int Lane) { 
        
    }





}
