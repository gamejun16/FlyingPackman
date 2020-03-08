using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseMissileController : MonoBehaviour
{
    /*****
     * 
     * SpecialBullet
     * 
     * 유도 미사일을 구현하는 스크립트
     * 
     * */
    AudioSource audioSource;
    static int specialBulletSerialNumb = 0;

    GameObject target;

    Animator animator;
    ObjectDropper objectDropper;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        objectDropper = GetComponent<ObjectDropper>();
        //uiStatusManager = GameObject.Find("GameManager").GetComponentInChildren<UIStatusManager>();

        target = GameObject.Find("PLAYER(follower)");
    }
    void Anim_Bomb()
    {
        animator.SetBool("isBomb", true);

        SoundManager.soundManager.playCreatureSound((int)SoundManager.creature.BOOM2, audioSource);
    }

    void Anim_Bomb_Done()
    {
        PoolingManager.poolingManager.returnSpecialBullet(gameObject, specialBulletSerialNumb);
        //Destroy(gameObject);
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

    // Update is called once per frame
    void Update()
    {
        move();
    }

    // 회전 담당(움직임은 ObjectDropper가 관리)
    void move()
    {
        float angle = (transform.position.x - target.transform.position.x) * 3f;
        transform.rotation = Quaternion.Euler(0, 0, -angle);

        // 매 Update마다 현 위치와 Ghost 위치의 중앙으로 이동(수렴)
        //transform.position = new Vector3((transform.position.x + ghost.transform.position.x) / 2, -4, 0);
        //transform.Translate((target.transform.position.x - transform.position.x) / 10, 0, 0, Space.World);
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
            PoolingManager.poolingManager.returnSpecialBullet(gameObject, specialBulletSerialNumb);
        }
        else if (collision.CompareTag("DestroyArea"))
        {
            PoolingManager.poolingManager.returnSpecialBullet(gameObject, specialBulletSerialNumb);
        }
    }
}
