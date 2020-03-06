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

    bool isOn;

    //SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 렌더링 끄기
        //spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn)
        {
            if (collision.CompareTag("bullet"))
            {
                Anim_Hitted();
                
                // 총알 삭제
                Destroy(collision.gameObject);
            }
        }
    }

    public void setIsOn(bool onOff)
    {
        isOn = onOff;
    }
    public bool getIsOn()
    {
        return isOn;
    }

    // 피격시
    void Anim_Hitted()
    {
        animator.SetBool("isHitted", true);

        // 사운드 출력
    }

    // 이펙트 종료
    void Anim_Hitted_Done()
    {
        animator.SetBool("isHitted", false);
    }

}


