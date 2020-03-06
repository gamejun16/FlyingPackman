using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BarrierController : MonoBehaviour
{
    /*****
     * 
     * Barrier 이펙트를 컨트롤하는 스크립트
     * 
     * 활성화일 경우, Sprite Renderer는 꺼진 상태를 유지한다
     * 공격이 들어오면 Sprite를 보이고 이펙트 사운드를 출력한다
     * 직후 Sprite를 off 한다
     * 
     * */

    bool isBarrierOn;

    //SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("hittedOff");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 배리어가 꺼지지 않는 버그를 해결하기위한 임시 대책
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBarrierOn)
        {
            if (collision.CompareTag("bullet"))
            {
                Anim_Hitted();

                // 총알 삭제
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("laser"))
            {
                Anim_Hitted();
            }

        }
    }
    

    public void setIsBarrierOn(bool onOff)
    {
        isBarrierOn = onOff;
    }
    public bool getIsOn()
    {
        return isBarrierOn;
    }

    // 피격시
    void Anim_Hitted()
    {
        // 피격시
        animator.SetBool("isHitted", true);

        // 사운드 출력
    }

    // 이펙트 종료
    void Anim_Hitted_Done()
    {
        animator.SetBool("isHitted", false);
    }

}


