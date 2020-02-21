/*
 * Copyright (c) 2015 Allan Pichardo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System;

public class SpawnNotes : MonoBehaviour
{
	public float timeBetweenSpawns = .2f;
	public int laneCount;
	float lastTimeSpawned = 0f;
	public float laneSpace = 1;
	public float hitRange = 1;
	public int lastSpawnedLane;
	float spawnHeight = 1;
	public float timeToHit = 2;



	void Start ()
	{
		//10 = 1.35
		spawnHeight = (float)(timeToHit) * 10; 


		//Select the instance of AudioProcessor and pass a reference
		//to this object
		AudioProcessor processor = FindObjectOfType<AudioProcessor> ();
		processor.onBeat.AddListener (onOnbeatDetected);
		processor.onSpectrum.AddListener (onSpectrum);

		for (int i = 0; i < laneCount; i++)
		{
			Vector3 spawnPos = new Vector3(transform.position.x + (i * 2) - ((laneCount / 2) * laneSpace), transform.position.y, transform.position.z);
			GameObject HitBox = (GameObject)Instantiate(Resources.Load("Hitbox"), spawnPos, Quaternion.identity);
			HitBox.GetComponent<NoteHitDetection>().lane = i;
		}
	}

	//this event will be called every time a beat is detected.
	//Change the threshold parameter in the inspector
	//to adjust the sensitivity
	void onOnbeatDetected (float[] spectrum)
	{


		float max = 0;
		int maxInt = 0;
		for (int i = 0; i < spectrum.Length; ++i)
		{
			if (spectrum[i] >= max)
			{
				max = spectrum[i];
				maxInt = i;
			}
		}

		if (Time.timeSinceLevelLoad - lastTimeSpawned >= timeBetweenSpawns)
		{
			maxInt = UnityEngine.Random.Range(0, laneCount);
			Vector3 spawnPos = new Vector3(transform.position.x + (maxInt * 2) - ((laneCount / 2) * laneSpace), transform.position.y + spawnHeight, transform.position.z);
			GameObject Note = (GameObject)Instantiate(Resources.Load("Note"), spawnPos, Quaternion.identity);
			Note.GetComponent<NoteHolder>().note = maxInt;
			lastTimeSpawned = Time.timeSinceLevelLoad;
			lastSpawnedLane = maxInt;

			//calls level gen script
			GameObject.FindGameObjectWithTag("LevelGen").GetComponent<LevelGenerator>().noteSpawned(maxInt);
		}
	}

	//This event will be called every frame while music is playing
	void onSpectrum (float[] spectrum)
	{
		//The spectrum is logarithmically averaged
		//to 12 bands

		for (int i = 0; i < spectrum.Length; ++i) {
			Vector3 start = new Vector3 (i, 0, 0);
			Vector3 end = new Vector3 (i, spectrum [i], 0);
			Debug.DrawLine (start, end);
		}



	}



}
