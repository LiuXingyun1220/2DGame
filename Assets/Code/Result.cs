using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //³É¹¦
        if (SceneManager.GetActiveScene().name == GameManager.SucceedScene)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //
            }
        }
        //Ê§°Ü
        if (SceneManager.GetActiveScene().name == GameManager.DefeatScene)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(GameManager.TopViewScene);
            }
        }
    }
}
