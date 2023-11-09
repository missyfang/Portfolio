using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha4))
        {
            if (SceneManager.GetActiveScene().name == "GoldScene")
                SceneManager.LoadScene("DungeonScene");
            else
                SceneManager.LoadScene("GoldScene");
        }
    }
}
