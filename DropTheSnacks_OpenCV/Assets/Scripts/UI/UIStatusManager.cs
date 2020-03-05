using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusManager : MonoBehaviour
{
    /*****
     * 
     * 화면에 표시되는 정보들을 관리하는 스크립트
     * 
     * 점수 HP 등을 출력한다.
     * 
     * */

    public Text score_t, hp_t, gage_t;
    public PlayerStatusManager playerStatusManager;

    public Image hp_i, gage_i;

    // Start is called before the first frame update
    void Start()
    {
        initUIStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // score의 증감을 관리
    public void scoreManager(bool isDecrease = false)
    {
        playerStatusManager.scoreManager(isDecrease);
        int s = playerStatusManager.getScore();
        score_t.text = "" + s;
    }

    // hp의 증감을 관리
    public void hpManager(bool isIncrease = false)
    {
        playerStatusManager.hpManager(isIncrease);
        int hp = playerStatusManager.getHP();
        hp_i.fillAmount = (float)hp/ PlayerStatusManager.__MAX_HP__;
        hp_t.text = "HP : " + hp;
    }

    // gage의 증감을 관리
    public void gageManager()
    {

        playerStatusManager.gageManager();
        int gage = playerStatusManager.getGAGE();
        gage_i.fillAmount = (float)gage / PlayerStatusManager.__MAX_GAGE__;
        gage_t.text = "GAGE : " + gage;
    }

    // Status를 초기화
    void initUIStatus()
    {
        playerStatusManager.initStatus();

        score_t.text = "Score : " + playerStatusManager.getScore();
        hp_t.text = "HP : " + playerStatusManager.getHP();
        gage_t.text = "GAGE : " + playerStatusManager.getGAGE();
    }
}
