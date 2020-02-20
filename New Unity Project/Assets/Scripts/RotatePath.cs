using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePath : MonoBehaviour
{
    [SerializeField]
    private Transform _objectToRotate;

    public Transform _pivot;

    public string _pathId;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        //print(_objectToRotate.eulerAngles.x);
    }

    private void Rotate()
    {
        _objectToRotate.RotateAround(_pivot.position, Vector3.left, (speed / Time.deltaTime) / 90f);
    }
}
