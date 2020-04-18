using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEater : MonoBehaviour
{
    /*****
     * 
     * 플레이어의 입에 부착되어 플레이어와 오브젝트간의 충돌을 체크하는 스크립트
     * 먹을 수 있는 오브젝트와 없는 오브젝트를 판단해 그 결과를 ## 로 전달한다
     * 
     * */

    //public UIStatusManager uiStatusManager;
    public PlayerAnimController playerAnimController;

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
        //if (collision.gameObject.layer == 8) // canEat
        //{
        //    if (collision.CompareTag("coin"))
        //    {
        //        UIStatusManager.uiStatusManager.gageManager();
        //    }
        //    else if (collision.CompareTag("heart"))
        //    {
        //        UIStatusManager.uiStatusManager.hpManager(true);
        //    }

        //    Destroy(collision.gameObject);
        //}


        if (collision.gameObject.layer == 9) // cannotEat
        {
            //UIStatusManager.uiStatusManager.hpManager();
            playerAnimController.Anim_Hitted();
            //Destroy(collision.gameObject);
        }
    }
    
}
