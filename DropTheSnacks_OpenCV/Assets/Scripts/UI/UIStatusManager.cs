using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusManager : MonoBehaviour
{
    /*****
     * 
     * 화면에 표시될 수 있는 모든 정보들을 관리하는 스크립트
     * 
     * 포함하는 정보:
     *  점수 HP 스코어(킬) 스테이지잔여시간
     * 
     * */

    int __stageTimer__ = 60; // 해당 초(sec) 경과 후 보스 스테이지가 시작된다
    
    // 본 스테이지의 남은 초(sec)
    int leftStageTime;

    // 현재 스테이지
    int curStage;
    
    public Text score_t, hp_t, gage_t, time_t;
    public PlayerStatusManager playerStatusManager;
    ObjectGenerator objectGenerator;
    
    public Image hp_i, gage_i, bossHp_i, bossHp_i_bg;

    private void Awake()
    {
        objectGenerator = GameObject.Find("GameManager").GetComponentInChildren<ObjectGenerator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        nextStage(true);
        StartCoroutine("stageTimer");
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
    public void gageManager(bool isInit = false)
    {
        if (isInit)
        {
            playerStatusManager.initGage();
        }
        else
        {
            playerStatusManager.gageManager();
        }
        int gage = playerStatusManager.getGAGE();
        gage_i.fillAmount = (float)gage / PlayerStatusManager.__MAX_GAGE__;
        gage_t.text = "GAGE : " + gage;
    }

    // 각 스테이지의 잔여 시간 체크 및 다음 스테이지로의 진행
    IEnumerator stageTimer()
    {
        float timer = 0f;

        while (leftStageTime > 0)
        {

            timer += Time.deltaTime;

            leftStageTime = __stageTimer__ - (int)timer;

            time_t.text = ""+leftStageTime;

            yield return null;
        }

        // 보스 스테이지 시작
        objectGenerator.bossStageStarter(curStage);
    }


    // Status들을 초기화
    void initUIStatus(bool isNextStage = false)
    {
        playerStatusManager.initStatus(isNextStage);

        leftStageTime = __stageTimer__;
        score_t.text = "" + playerStatusManager.getScore();
        hp_t.text = "HP : " + playerStatusManager.getHP();
        gage_t.text = "GAGE : " + playerStatusManager.getGAGE();
    }
  
    // 다음 스테이지로 진행할 수 있는 스크립트
    public void nextStage(bool isInit = false)
    {
        if (isInit) curStage = 1;
        else curStage++;

        // 스테이지가 초기화(다시시작)되는 경우와
        // 다음 스테이지로 진행되는 경우를 구분한다
        initUIStatus(!isInit);
    }


}
