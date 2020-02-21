using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHitDetection : MonoBehaviour
{
    public int lane;
    float hitRange = 1;
    bool noteInRange = false;
    GameObject note;

    private void Start()
    {
        hitRange = GameObject.FindGameObjectWithTag("Processor").GetComponent<SpawnNotes>().hitRange;
        hitRange = 3;
    }

    private void Update()
    {

        for (int i = 0; i < GameObject.FindGameObjectWithTag("Processor").GetComponent<SpawnNotes>().laneCount; i++)
        {
            if (Input.GetButtonDown("Lane" + i.ToString()))
            {
                foreach (GameObject note in GameObject.FindGameObjectsWithTag("Note"))
                {
                    foreach (GameObject hitBox in GameObject.FindGameObjectsWithTag("HitBox"))
                    {
                        if (Vector3.Distance(hitBox.transform.position, note.transform.position) <= hitRange)
                        {
                            if (hitBox.GetComponent<NoteHitDetection>().lane == i && note.GetComponent<NoteHolder>().note == i)
                            {
                                Debug.Log(note.GetComponent<NoteHolder>().timeSinceCreated);
                                noteInRange = true;
                                Destroy(note);
                                OnNoteHit();
                            }
                        }
                    }
                }

                if (!noteInRange)
                {
                    OnNoteMiss();
                }

            }

        }



        
    }

    void OnNoteHit() {
        //Debug.Log("Hit");
    }

    void OnNoteMiss() {
        Debug.Log("Miss");
    }

}
