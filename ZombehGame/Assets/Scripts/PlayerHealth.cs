using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SubtractHealth(float amount)
    {
      health -= amount;
    }
}
