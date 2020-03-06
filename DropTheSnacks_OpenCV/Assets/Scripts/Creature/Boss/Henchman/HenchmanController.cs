using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenchmanController : MonoBehaviour
{
    /****
     * 
     * 보스를 공격하기 위해 제거해야하는 하수인을 컨트롤하는 스크립트
     * 
     * 스스로 파괴될 때까지 미사일을 투하한다?
     * 
     * */

    UIStatusManager uiStatusManager;
    BossPatternManager bossPatternManager;

    Animator animator;

    public GameObject missile;


    private void Awake()
    {
        uiStatusManager = GameObject.Find("GameManager").GetComponentInChildren<UIStatusManager>();
        bossPatternManager = GetComponentInParent<BossPatternManager>();

        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("missileDropper");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Anim_Bomb()
    {
        animator.SetBool("isBomb", true);
    }

    void Anim_Bomb_Done()
    {
        bossPatternManager.henchmanKilled();
        Destroy(gameObject);
    }

    // Creature가 터지는 애니메이션이 시작됨과 동시에 킬이 적립된다.
    void playerGetKillPoint()
    {
        // 플레이어 점수 획득
        uiStatusManager.scoreManager();
    }
    

    IEnumerator missileDropper()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > 1.0f)
            {
                timer = 0f;

                Instantiate(missile, transform.position, Quaternion.identity);
            }
            yield return null;
        }
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
        else if (collision.CompareTag("bullet")) // laser
        {
            // 낙하 정지
            GetComponent<ObjectDropper>().speed = 0;

            // 폭발 애니메이션
            Anim_Bomb();

            // 총알 삭제
            Destroy(collision.gameObject);

            // 이후 Anim_Bomb_Done() 호출 및 자동 Destroy()
        }
    }

}
