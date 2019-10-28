using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MAZE PROC GEN LAB
// all students: complete steps 1-6, as listed in this file
// optional: if you have extra time, complete the "extra tasks" to do at the very bottom

// STEP 1: ======================================================================================
// put this script on a Sphere... it will move around, and drop a path of floor tiles behind it

public class Pathmaker : MonoBehaviour
{
	public static int globalTileCount;
	public Transform pathmakerSpherePrefab;
	public Transform[] TileList;

	[Space(5)] [Header("Generation Values")]
	public int maxTilesToSpawn = 50;
	public int minTilesToSpawn;
	[Range(0, 1)] public float turnRightChance;
	[Range(0, 1)] public float turnLeftChance;
	[Range(0, 1)] public float minSpawnChance;
	[Range(0, 1)] public float maxSpawnChance;

	private int tilesToSpawn;
	private float spawnChance;
	private int counter;

	private void Start()
	{
		tilesToSpawn = Random.Range(minTilesToSpawn, maxTilesToSpawn);
		spawnChance = Random.Range(minSpawnChance, maxSpawnChance);
	}

	void Update () {

		if (counter < tilesToSpawn || globalTileCount < 500)
		{
			float rand = Random.value;
			
			if (rand <= turnRightChance)
				transform.Rotate(0, 90, 0);
			else if(rand <= turnRightChance + turnLeftChance)
				transform.Rotate(0, -90, 0); 
			else if (rand >= spawnChance && rand <= 1f)
				Instantiate(pathmakerSpherePrefab, transform.position, Quaternion.identity);

			//Make a random chance for something besides a normal road tile to spawn
			float specialTileChance = Random.Range(0, 10);
			if (specialTileChance < 2)
			{
				Instantiate(TileList[Random.Range(1, TileList.Length)], transform.position, transform.rotation);
			}
			else
			{
				Instantiate(TileList[0], transform.position, Quaternion.identity);
			}

			transform.Translate(transform.forward * 5, Space.World);

			counter++;
			globalTileCount++;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	

}

// OPTIONAL EXTRA TASKS TO DO, IF YOU WANT: ===================================================

// DYNAMIC CAMERA:
// position the camera to center itself based on your generated world...
// 1. keep a list of all your spawned tiles
// 2. then calculate the average position of all of them (use a for() loop to go through the whole list) 
// 3. then move your camera to that averaged center and make sure fieldOfView is wide enough?

// BETTER UI:
// learn how to use UI Sliders (https://unity3d.com/learn/tutorials/topics/user-interface-ui/ui-slider) 
// let us tweak various parameters and settings of our tech demo

// WALL GENERATION
// add a "wall pass" to your proc gen after it generates all the floors
// 1. raycast downwards around each floor tile (that'd be 8 raycasts per floor tile, in a square "ring" around each tile?)
// 2. if the raycast "fails" that means there's empty void there, so then instantiate a Wall tile prefab
// 3. ... repeat until walls surround your entire floorplan
// (technically, you will end up raycasting the same spot over and over... but the "proper" way to do this would involve keeping more lists and arrays to track all this data)
