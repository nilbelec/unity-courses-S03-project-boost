using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector;

    // todo remove from inspector later
    [Range(0f, 1f)]
    [SerializeField]
    float movementFactor; // 0 for not moved, 1 for fully moved

    private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
	}
}
