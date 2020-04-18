using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    /*****
     * 
     * 아이템의 삭제 등을 컨트롤하는 스크립트
     * 
     * */

    //AudioSource audioSource;

    enum SERIAL { COIN, HEART, }

     // serialNumb으로 각 아이템 구분
    // 0: coin, 1:heart, ...
    public int serialNumb;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 획득 및 해당 아이템의 효과 발생
    void itemEffect()
    {
        // 효과 발생
        switch (serialNumb)
        {
            case (int)SERIAL.COIN:
                UIStatusManager.uiStatusManager.gageManager();
                SoundManager.soundManager.playPlayerSound((int)SoundManager.player.COIN, UIStatusManager.uiStatusManager.itemAudioSource);
                break;
            case (int)SERIAL.HEART:
                UIStatusManager.uiStatusManager.hpManager(true);
                SoundManager.soundManager.playPlayerSound((int)SoundManager.player.HEART, UIStatusManager.uiStatusManager.itemAudioSource);
                break;
        }

        // 아이템 풀 반환
        PoolingManager.poolingManager.returnItem(gameObject, serialNumb);
    }

    // 아이템이 화면 밖으로 벗어났거나 플레이어가 획득한 경우 삭제하기위한 구문
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 획득
        if (collision.CompareTag("player"))
        {
            itemEffect();
            Debug.Log("아이템 획득");
        }

        // 삭제
        else if (collision.CompareTag("DestroyArea"))
        {
            // 아이템 반환
            PoolingManager.poolingManager.returnItem(gameObject, serialNumb);
        }
    }
}
