using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class generateLevels : MonoBehaviour {

	List<GameObject> levelBricks = new List<GameObject> ();

	string[] direction = {"up", "down", "left", "right"};

	[SerializeField]
	int numberOfRooms = 4;

	string[] rooms = {

		"room.oneway.up.small",
		"room.oneway.up.bigger"
	};

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numberOfRooms; i++) 
		{
			string brickType = rooms[Random.Range(0, rooms.Length)];
			GameObject brick = (GameObject)Instantiate(Resources.Load(brickType));
			levelBricks.Add(brick);
		}

		for (int i = 1; i < levelBricks.Count; i++) {
			string moveDirection = direction[i-1];
			moveBrick(levelBricks[0], levelBricks[i], moveDirection);
		}
	}
	
	// Update is called once per frame
	void Update () {

		Debug.DrawRay (new Vector3(0, 2, 0), new Vector3 (2, 0, 0), Color.red);
		Debug.DrawRay (new Vector3(0, 2, 0), new Vector3 (0, 0, 2), Color.blue);
		// Debug.DrawRay(levelBricks[1].transform.position, new Vector3(0, 0, levelBricks[1].collider.bounds.size.z ));
	}

	private Vector3 getBrickPosition(GameObject brick)
	{
		return brick.transform.position;
	}

	void rotateBrickAroundAngle(GameObject brick, float angle)
	{
		brick.transform.RotateAround (brick.transform.position, Vector3.up, angle);
	}

	void moveBrick(GameObject parentBrick, GameObject movedBrick, string direction)
	{
		float x = 0;
		float y = 0;
		float z = 0;

		if (direction == "up") {
			x = movedBrick.transform.position.x;
			y = movedBrick.transform.position.y;
			z = movedBrick.transform.position.z + parentBrick.collider.bounds.size.z / 2
				+ movedBrick.collider.bounds.size.z / 2;
		}

		if (direction == "down") {
			x = movedBrick.transform.position.x;
			y = movedBrick.transform.position.y;
			z = movedBrick.transform.position.z - parentBrick.collider.bounds.size.z / 2
				- movedBrick.collider.bounds.size.z / 2;
			rotateBrickAroundAngle(movedBrick, 180);
		}

		if (direction == "left") {
			x = movedBrick.transform.position.x - parentBrick.collider.bounds.size.x / 2
				- movedBrick.collider.bounds.size.x / 2;
			y = movedBrick.transform.position.y;
			z = movedBrick.transform.position.z;
			rotateBrickAroundAngle(movedBrick, 270);
		}

		if (direction == "right") {
			x = movedBrick.transform.position.x + parentBrick.collider.bounds.size.x / 2
				+ movedBrick.collider.bounds.size.x / 2;
			y = movedBrick.transform.position.y;
			z = movedBrick.transform.position.z;
			rotateBrickAroundAngle(movedBrick, 90);
		}

		movedBrick.transform.position = new Vector3(x, y, z);
	}

}
