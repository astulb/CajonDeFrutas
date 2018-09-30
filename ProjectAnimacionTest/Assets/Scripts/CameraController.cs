using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float smoothing = 10;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, smoothing * Time.deltaTime);
    }
}
