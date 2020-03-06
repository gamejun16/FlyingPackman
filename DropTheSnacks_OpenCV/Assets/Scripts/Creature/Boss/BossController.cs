using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    /*****
     * 
     * 보스의 움직임(피격 등)을 제어하기위한 스크립트
     * 
     * */

    UIStatusManager uiStatusManager;
    BarrierController barrierController;
    BossStatusManager bossStatusManager;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        uiStatusManager = GameObject.Find("GameManager").GetComponentInChildren<UIStatusManager>();
        barrierController = GetComponentInChildren<BarrierController>();
        bossStatusManager = GetComponent<BossStatusManager>();
    }

    private void Start()
    {
        StartCoroutine("hittedOff");
    }

    void Anim_Hitted()
    {
        if (!animator.GetBool("isHitted"))
            animator.SetBool("isHitted", true);
    }

    void Anim_Hitted_Done()
    {
        if (!animator.GetBool("isBomb"))
        {
            animator.SetBool("isHitted", false);
        }
    }

    public void Anim_Bomb()
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
         // 방어막이 꺼져있을 때에만 체크
        if (!barrierController.getIsOn())
        {
            if (collision.CompareTag("laser")) // laser
            {
                // 피격 애니메이션
                Anim_Hitted();

                // 피격
                bossStatusManager.hpDecrease(10);

                // 이후 Anim_Bomb_Done() 호출 및 자동 Destroy()
            }
            else if (collision.CompareTag("bullet")) // laser
            {
                // 피격 애니메이션
                Anim_Hitted();

                // 피격
                bossStatusManager.hpDecrease();

                Debug.Log("으악");

                // 총알 삭제
                Destroy(collision.gameObject);

                // 이후 Anim_Bomb_Done() 호출 및 자동 Destroy()
            }
        }
    }

    // 피격 애니메이션이 종료되지 않는 버그를 해결하기위한 임시 대책
    IEnumerator hittedOff()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            if (timer > 0.3f)
            {
                timer = 0f;
                animator.SetBool("isHitted", false);
            }
            yield return null;
        }
    }
}
