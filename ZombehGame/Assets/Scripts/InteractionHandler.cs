using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask layerMask;
    [SerializeField] AudioClip ammoPickup;
    [SerializeField] TextMeshProUGUI e;
    [SerializeField] float interactionRange;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Debug.DrawRay(Camera.main.transform, Camera.main.transform.forward,);
        Physics.Raycast(ray, out hit, 100);
        e.enabled = false;
        if (Physics.Raycast(ray, out hit, interactionRange, layerMask))
        {
            var pickup = hit.transform.gameObject;
            e.enabled = true;
            if (pickup.tag == "Ammo")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    audioSource.PlayOneShot(ammoPickup);
                    FindObjectOfType<Gun>().AddAmmo(15);
                    Destroy(pickup);
                }
            }
        }
    }
}
