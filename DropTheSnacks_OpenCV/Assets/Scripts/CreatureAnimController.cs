using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class CreatureAnimController : MonoBehaviour
{
    /*****
     * 
     * 이게 꼭 필요한 스크립트인가?
     * Animation Event Flag에서 함수를 호출하기위해 Creature에 적용되는 스크립트
     * 
     * 스스로를 폭발시키고 Destroy된다
     * 
     * */

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Anim_Bomb()
    {
        animator.SetBool("isBomb", true);
    }

    void Anim_Bomb_Done()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("laser")) // laser
        {
            // 낙하 정지
            GetComponent<ObjectDropper>().speed = 0;

            // 폭발 애니메이션
            Anim_Bomb();

            // 이후 Anim_Bomb_Done() 호출 및 자동 Destroy()
        }
    }
}
