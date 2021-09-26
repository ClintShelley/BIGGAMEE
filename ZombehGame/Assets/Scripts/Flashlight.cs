using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light flashLight;
    bool on;
    [SerializeField] AudioClip clickNoise;
    AudioSource audioSource;
    void Start()
    {
        flashLight.intensity = 0;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessLight();
    }
    void ProcessLight()
    {
        if (Input.GetKeyDown(KeyCode.F) && on == false)
        {
            on = true;
            flashLight.intensity =  3;
            audioSource.PlayOneShot(clickNoise);
        }
        else if (Input.GetKeyDown(KeyCode.F) && on == true)
        {
            on = false;
            flashLight.intensity = 0;
            audioSource.PlayOneShot(clickNoise);
        }
    }
}
