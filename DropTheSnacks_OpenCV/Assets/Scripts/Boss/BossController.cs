using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    UIStatusManager uiStatusManager;
    BarrierController barrierController;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        uiStatusManager = GameObject.Find("GameManager").GetComponentInChildren<UIStatusManager>();
        barrierController = GetComponentInChildren<BarrierController>();
    }

    void Anim_Bomb()
    {
        animator.SetBool("isBomb", true);
    }

    void Anim_Bomb_Done()
    {

        Destroy(gameObject);
    }

    // Creature가 터지는 애니메이션이 시작됨과 동시에 킬이 적립된다.
    void playerGetKillPoint()
    {
        // 플레이어 점수 획득
        uiStatusManager.scoreManager();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.CompareTag("laser")) // laser
            {
                // 폭발 애니메이션
                Anim_Bomb();

                // 이후 Anim_Bomb_Done() 호출 및 자동 Destroy()
            }

        // 방어막이 꺼져있을 때에만 체크
        if (!barrierController.getIsOn())
        {

            if (collision.CompareTag("bullet")) // laser
            {
                // 폭발 애니메이션
                Anim_Bomb();

                // 총알 삭제
                Destroy(collision.gameObject);

                // 이후 Anim_Bomb_Done() 호출 및 자동 Destroy()
            }
        }
    }
}
