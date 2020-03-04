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
        score_t.text = "Score : " + playerStatusManager.getScore();
    }

    // hp의 증감을 관리
    public void hpManager(bool isIncrease = false)
    {
        playerStatusManager.hpManager(isIncrease);
        hp_t.text = "HP : " + playerStatusManager.getHP();
    }

    // gage의 증감을 관리
    public void gageManager(bool isDecrease = false)
    {
        playerStatusManager.gageManager(isDecrease);
        gage_t.text = "GAGE : " + playerStatusManager.getGAGE();
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
