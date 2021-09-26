using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100;
    [SerializeField] TextMeshProUGUI healthDisplay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = health.ToString();
    }
    public void SubtractHealth(float amount)
    {
      health -= amount;
    }
}
