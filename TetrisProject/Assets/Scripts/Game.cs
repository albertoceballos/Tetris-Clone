using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject[] shapes;

    public GameObject[] nextShapes;

    GameObject upNextObject = null;

    int shapeIndex = 0;
    int nextShapeIndex = 0;

	// Use this for initialization
	void Start () {
        nextShapeIndex = Random.Range(0, 6);
        SpawnShape();
	}

    public void SpawnShape() {
        int shapeIndex = nextShapeIndex;
        
        Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);

        nextShapeIndex = Random.Range(0, 6);

        Vector3 nextShapePos = new Vector3(13.2f, 16f, 0);

        if (upNextObject != null) {
            Destroy(upNextObject);
        }

        upNextObject = Instantiate(nextShapes[nextShapeIndex], nextShapePos, Quaternion.Euler(0,0,90));
    }
}
