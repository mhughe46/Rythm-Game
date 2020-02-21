using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHolder : MonoBehaviour
{
    public int note;
    float timeCreated;
    public float timeSinceCreated;

    private void Start()
    {
        timeCreated = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        timeSinceCreated = Time.timeSinceLevelLoad - timeCreated;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.down*10;
    }



}
