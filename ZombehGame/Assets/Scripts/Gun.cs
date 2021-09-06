using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float impactForce = 30f;
    [SerializeField] float fireRate = 15f; // greater the fire rate, the less time between shots
   
    [SerializeField] AudioClip gunShot;

    [SerializeField] ParticleSystem cartridgeEject;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject metalImpact;
    
    public Camera fpsCam;
    float nextTimetoFire = 0f;
    float timeToRemoveImpact = 100f;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        cartridgeEject.Play();
        audioSource.PlayOneShot(gunShot);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal);
            }
            var hitEffect = metalImpact;
            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal * impactForce));
            if (Time.time >= timeToRemoveImpact)
            {
                timeToRemoveImpact = 0;
                DestroyImmediate(metalImpact, true);
            }
            timeToRemoveImpact += Time.deltaTime;
         
        }
    }
}
