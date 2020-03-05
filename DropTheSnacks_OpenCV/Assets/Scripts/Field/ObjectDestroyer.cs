using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    /****
      * 
      * 화면 아래로 벗어난 오브젝트를 삭제하는 스크립트
      * 
      * */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            // 화면 밖으로 벗어난 오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }

}
