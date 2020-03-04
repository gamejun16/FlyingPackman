using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // -5 ~ 5 to 
        float angle = (transform.position.x - ghost.transform.position.x) * 15f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 매 Update마다 현 위치와 Ghost 위치의 중앙으로 이동(수렴)
        //transform.position = new Vector3((transform.position.x + ghost.transform.position.x) / 2, -4, 0);
        transform.Translate((ghost.transform.position.x - transform.position.x) / 10, 0, 0, Space.World);
        

    }
}
