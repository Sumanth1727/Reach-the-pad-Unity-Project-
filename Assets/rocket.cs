using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class rocket : MonoBehaviour
{

    Rigidbody rb;
    AudioSource As;
    public float RotationSpeed = 100f;
    [SerializeField] float BoostMag = 100f;
    enum State {Alive,Dead,LoadingNextLevel};
    State state = State.Alive;
    [SerializeField] AudioClip MainTrust;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip SucessSound;
    [SerializeField] ParticleSystem MainThrustParticle;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem SucessParticle;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        As = GetComponent<AudioSource>();
    }




    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }




    void OnCollisionEnter(Collision collision)
    {
        if(state!=State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
            {

            case "Finish":
                SucessParticle.Play();
                state = State.LoadingNextLevel;
                As.Stop();
                As.PlayOneShot(SucessSound);
                Invoke("LoadNewScene", 1f);
                break;
            case "Friendly":    
                print("hit friendly object");
                break;
            default:
                MainThrustParticle.Stop();
                DeathParticle.Play();
                As.Stop();
                As.PlayOneShot(DeathSound);
                state = State.Dead;
                print("non-friendly object collided");
                Invoke("LoadSameLevel", 1f);
                break;
        }

       
    }






    private void LoadSameLevel()
    {
        SucessParticle.Stop();
        DeathParticle.Stop();
        state = State.Alive;
        int y = SceneManager.GetActiveScene().buildIndex;
        As.Stop();
        SceneManager.LoadScene(y);
    }






    private  void LoadNewScene()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        state = State.Alive;
        As.Stop();
        try
        { 
            SceneManager.LoadScene(y+1);
        }

        catch (Exception e)
        {
            print("idiot");
            print(e);
            SceneManager.LoadScene(0);
        }
       
    }






    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up* BoostMag);
            if (!As.isPlaying)
            {
                As.PlayOneShot(MainTrust);
            }
            MainThrustParticle.Play();
        }
        else
        {
            MainThrustParticle.Stop();
            As.Stop();
        }
    }





private void Rotate()
    {
        rb.freezeRotation = true;

        float rotationThisFrame = RotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward* rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward* rotationThisFrame);
        }
        rb.freezeRotation = false;
    }

}
