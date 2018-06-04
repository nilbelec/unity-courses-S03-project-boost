using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    enum State { Alive, Dying, Trascending }

    State state = State.Alive;

    new Rigidbody rigidbody;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive){
            Thrust();
            Rotate();
        } else if (audioSource.isPlaying) {
            audioSource.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
            return;
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Trascending;
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float impulseThisFrame = mainThrust * Time.deltaTime;
            rigidbody.AddRelativeForce(Vector3.up * impulseThisFrame);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void Rotate()
    {
        if (state == State.Dying)
            return;
        rigidbody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false;
    }
}
