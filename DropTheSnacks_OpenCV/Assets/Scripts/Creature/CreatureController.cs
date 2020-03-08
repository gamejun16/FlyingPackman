using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class CreatureController : MonoBehaviour
{
    /*****
     * 
     * Animation Event Flag에서 함수를 호출하기위해 Creature에 적용되는 스크립트
     * 
     * 스스로를 폭발시키고 Destroy된다
     * 
     * */

    Animator animator;
    ObjectDropper objectDropper;

    AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        objectDropper = GetComponent<ObjectDropper>();
        audioSource = GetComponent<AudioSource>();
    }

    void Anim_Bomb()
    {
        animator.SetBool("isBomb", true);

        SoundManager.soundManager.playCreatureSound((int)SoundManager.creature.BOOM, audioSource);
    }

    void Anim_Bomb_Done()
    {
        PoolingManager.poolingManager.returnCreature(gameObject, 0);
    }

    // Creature가 터지는 애니메이션이 시작됨과 동시에 킬이 적립된다.
    void playerGetKillPoint()
    {
        // 플레이어 점수 획득
        UIStatusManager.uiStatusManager.scoreManager();
    }

    private void OnEnable()
    {
        objectDropper.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("laser")) // laser
        {
            // 낙하 정지
            objectDropper.enabled = false;

            // 폭발 애니메이션
            Anim_Bomb();

            // 이후 Anim_Bomb_Done() 호출 및 자동 poolReturn
        }
        else if (collision.CompareTag("bullet")) // laser
        {
            // 낙하 정지
            objectDropper.enabled = false;

            // 폭발 애니메이션
            Anim_Bomb();

            // 총알 삭제
            //Destroy(collision.gameObject);

            // 이후 Anim_Bomb_Done() 호출 및 자동 poolReturn
        }


        if (collision.CompareTag("player"))
        {
            UIStatusManager.uiStatusManager.hpManager();
            PoolingManager.poolingManager.returnCreature(gameObject, 0);
        }
        else if (collision.CompareTag("DestroyArea"))
        {
            PoolingManager.poolingManager.returnCreature(gameObject, 0);
        }
    }
}
