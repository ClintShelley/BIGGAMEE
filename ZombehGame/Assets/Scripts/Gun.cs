using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float impactForce = 30f;
    [SerializeField] float fireRate = 15f; // greater the fire rate, the less time between shots
   
    [SerializeField] AudioClip gunShot;
    [SerializeField] AudioClip reload;
    [SerializeField] AudioClip dryFire;

    [SerializeField] ParticleSystem cartridgeEject;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject metalImpact;
    [SerializeField] GameObject bloodImpact;
    
    public Camera fpsCam;
    float nextTimetoFire = 0f;
    float timeToRemoveImpact = 100f;
    AudioSource audioSource;

    [SerializeField] Recoil recoilCam;
    [SerializeField] Recoil recoilGun;
    Animator animator;
    bool ableToFire = true;

    [SerializeField] int startingAmmo;
    int totalAmmo;
    int currentMag;
    [SerializeField] Vector3 aimPosition;
    [SerializeField] float aimSpeed;
    Vector3 hipfirePosition;
    [SerializeField] GameObject crosshair;
    bool reloading = false;
    [SerializeField] TextMeshProUGUI ammoDisplay;

    int ammoPerMag = 15;

    float startFov;

    public void AddAmmo(int amount)
    {
        totalAmmo += amount;
    }
    private void AimDownSights()
    {

        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aimSpeed);
            recoilGun.aim = true;
            recoilCam.aim = true;
            crosshair.SetActive(false);
            Camera.main.fieldOfView = Mathf.Lerp(90, 75, 1f);

        }
        else
        {

            transform.localPosition = Vector3.Lerp(transform.localPosition, hipfirePosition, Time.deltaTime * aimSpeed);
            recoilGun.aim = false;
            recoilCam.aim = false;
            crosshair.SetActive(true);
            Camera.main.fieldOfView = Mathf.Lerp(75, 90, 1f);
        }
    }
    void Start()
    {
        hipfirePosition = transform.localPosition;
        currentMag = 15;
        totalAmmo += startingAmmo;
        startFov = Camera.main.fieldOfView;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void DisplayAmmo()
    {
        ammoDisplay.text = currentMag + "/" + totalAmmo;
    }

    void ProcessReload()
    {
        int bulletsToLoad = ammoPerMag - currentMag;
        int bulletsToDeduct = (totalAmmo >= bulletsToLoad) ? bulletsToLoad : totalAmmo;

        totalAmmo -= bulletsToDeduct;
        currentMag += bulletsToDeduct;
    }
    public void ToggleFireable()
    {
        ableToFire = !ableToFire;
        reloading = !reloading;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimetoFire && !reloading && currentMag > 0 )
        {
            nextTimetoFire = Time.time + 1f/fireRate;
            Shoot();
            print(currentMag);
        }
        if (Input.GetButtonDown("Fire1") && currentMag == 0 && Time.time >= nextTimetoFire && !reloading)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
            audioSource.PlayOneShot(dryFire);
        }
        if (Input.GetKeyDown("r") && !reloading && totalAmmo > 0 && Time.time >= nextTimetoFire && currentMag != 15)
        {
            animator.Play("Reload", -1, 0);
            ProcessReload();
        }
        AimDownSights();
        DisplayAmmo();
    }

    public void PlayReloadSound()
    {
        audioSource.PlayOneShot(reload);
    }

    void Shoot()
    {
        currentMag--;
        animator.Play("Fire",-1,0);
        recoilGun.Fire();
        recoilCam.Fire();
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
            if (hit.transform.tag == "Enemy")
            {
                 hitEffect = bloodImpact;
                timeToRemoveImpact = 100f;
            }
            else
            {
                 hitEffect = metalImpact;
                timeToRemoveImpact = 100f;
            }
          
       
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
