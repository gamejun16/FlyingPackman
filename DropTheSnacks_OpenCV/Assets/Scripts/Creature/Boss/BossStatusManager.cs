using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatusManager : MonoBehaviour
{
    /*****
     * 
     * Boss의 체력의 변동 및 체력바(UI) 등을 관리하는 스크립트
     * 
     * Status에 포함되는 항목
     *  HP, 
     * 
     * */

    public int __MAX_HP__ = 40;

    UIStatusManager uiStatusManager;
    BossController bossController;

    int hp;
    Image hp_i, hp_i_bg;

    private void Awake()
    {
        uiStatusManager = GameObject.Find("GameManager").GetComponentInChildren<UIStatusManager>();
        bossController = GetComponent<BossController>();

        hp_i = uiStatusManager.bossHp_i;
        hp_i_bg = uiStatusManager.bossHp_i_bg;
    }

    // Start is called before the first frame update
    void Start()
    {
        battleManager(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // 보스 전투가 시작되고 끝날 때 호출되는 함수
    public void battleManager(bool isStart = true)
    {
        hp_i.enabled = isStart;
        hp_i_bg.enabled = isStart;

        hp = __MAX_HP__;
    }

    public void hpDecrease(int damage = 1)
    {
        hp -= damage;
        hp_i.fillAmount = (float)hp / __MAX_HP__;

        if(hp <= 0)
        {
            battleManager(false);

            bossController.Anim_Bomb();
        }
    }

}
