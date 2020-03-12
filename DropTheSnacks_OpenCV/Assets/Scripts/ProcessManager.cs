using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    /*****
     * 
     * 씬 및 프로세스를 제어하는 스크립트
     * 
     * */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
