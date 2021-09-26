using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject gameOverUI;
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerHealth>().health <= 0)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            gameOverUI.SetActive(true);
        }
    }
}
