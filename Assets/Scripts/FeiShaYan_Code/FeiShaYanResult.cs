using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeiShaYanResult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //³É¹¦
        if (SceneManager.GetActiveScene().name == FeiShaYanManager.SucceedScene)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //
            }
        }
        //Ê§°Ü
        if (SceneManager.GetActiveScene().name == FeiShaYanManager.DefeatScene)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(FeiShaYanManager.TopViewScene);
            }
        }
    }
}
