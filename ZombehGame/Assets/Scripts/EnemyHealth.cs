using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float enemyHealth = 50f;
    [SerializeField] AudioClip zombie1;
    [SerializeField] AudioClip zombie2;
    [SerializeField] AudioClip zombie3;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        int randomSound = Random.Range(1, 3);
            switch (randomSound)
            {
                case 1:
                    print("playedsound1");
                    audioSource.PlayOneShot(zombie1);
                    break;
                case 2:
                    print("playedsound2");
                    audioSource.PlayOneShot(zombie2);
                    break;
                case 3:
                    print("playedsound3");
                    audioSource.PlayOneShot(zombie3);
                    break;
            }
        Destroy(gameObject,1f);
    }

    public float ReturnHealth()
    {
        return enemyHealth;
    }

}
