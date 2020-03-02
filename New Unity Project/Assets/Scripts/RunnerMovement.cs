using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _laneMiddle = null;

    [SerializeField]
    private Transform _laneRight = null;

    [SerializeField]
    private Transform _laneLeft = null;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private int _lane;

    // Start is called before the first frame update
    void Start()
    {
        _laneLeft = GameObject.Find("LeftLane").transform;
        _laneRight = GameObject.Find("RightLane").transform;
        _laneMiddle = GameObject.Find("MiddleLane").transform;

        _lane = Random.Range(-1, 2);
        _movementSpeed = Random.Range(1f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        GoToSide();
    }

    void GoToSide()
    {
        float distance = 0;
        if (_lane < 0)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(_laneLeft.position.x, this.transform.position.y, this.transform.position.z), Time.deltaTime * _movementSpeed);
            distance = Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(_laneLeft.position.x, 0, 0));
        }
        else if (_lane == 0)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(_laneMiddle.position.x, this.transform.position.y, this.transform.position.z), Time.deltaTime * _movementSpeed);
            distance = Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(_laneMiddle.position.x, 0, 0));
        }
        else if (_lane > 0)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(_laneRight.position.x, this.transform.position.y, this.transform.position.z), Time.deltaTime * _movementSpeed);
            distance = Vector3.Distance(new Vector3(this.transform.position.x, 0, 0), new Vector3(_laneRight.position.x, 0, 0));
        }

        if (distance <= 0.2f)
        {
            _lane = Random.Range(-1, 2);
        }
    }
}
