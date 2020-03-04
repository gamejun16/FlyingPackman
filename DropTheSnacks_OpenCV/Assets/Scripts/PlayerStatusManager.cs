using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    /*****
     * 
     * 플레이어의 상태를 관리 및 저장하는 스크립트(Not monoBehaviour
     * 저장되는 정보 목록 :
     *      Score , HP , Gage etc.
     * 
     * */

    public PlayerAnimController playerAnimController;

    // 시작 HP
    static int __HP__ = 3;
    // 최대 GAGE
    static int __FULL_GAGE__ = 5;

    int SCORE; // 점수
    int HP; // 체력
    int GAGE; // 게이지 - 노란 공을 일정 개수 이상 먹으면 적을 먹을 수 있다
    
    // Status들을 초기화하는 함수
    public void initStatus()
    {
        SCORE = 0;
        HP = __HP__;
        GAGE = 0;
    }

    public int getScore()
    {
        return SCORE;
    }
    public int getHP()
    {
        return HP;
    }
    public int getGAGE()
    {
        return GAGE;
    }


    public void scoreManager(bool isDecrease = false)
    {
        if (isDecrease)
        {
            SCORE--;
        }
        else
        {
            SCORE++;
        }
    }

    public void hpManager(bool isIncrease = false)
    {
        if (isIncrease)
        {
            HP++;
        }
        else
        {
            HP--;
        }
    }

    public void gageManager(bool isFull = false)
    {
        if (isFull)
        {
            playerAnimController.Anim_PowerUP();
            GAGE = 0;
        }
        else
        {
            GAGE++;
            if(GAGE == __FULL_GAGE__)
            {
                gageManager(true);
            }
        }
    }
}
