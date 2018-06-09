using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);

    [Range(0.5f, 10f)]
    [SerializeField] float period = 2f;

    private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (period <= 0.5f)
            period = 0.5f;
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);

        float movementFactor = (rawSinWave / 2f) + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
	}
}
