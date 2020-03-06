using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager: MonoBehaviour
{
    /*****
     * 
     * 각 스테이지의 보스의 패턴을 관리하는 스크립트
     * 
     * hp 정보,
     * Henchman(하수인)들을 반복 소환하는 등의 구문을 포함한다
     * 
     * 보스는 소환된 이후, 파괴될 때까지 아래 프로세스를 반복한다
     *  # 1.
     *  하수인을 소환한다
     *  # 1-2.
     *  하수인이 파괴되기 전 까지는 보호막을 가지며 피해를 입지 않는다
     *  # 2.
     *  하수인이 파괴되면 피해를 입는다
     *  # 3.
     *  일정 수치 이상의 피해(혹은 시간)가 진행되면 #1로 돌아간다
     * 
     * */

    // 하수인
    public GameObject henchman;

    // 남은 하수인 수
    int leftHenchman;

    // 방어막
    BarrierController barrierController;
    

    // 하수인 스폰 포인트
    public List<GameObject> henchmanSpawnPoints;

    private void Awake()
    {
        barrierController = GetComponentInChildren<BarrierController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 테스트용 베리어 온
        barrierController.setIsOn(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void henchmanSpawner()
    {
        // 하수인 소환
        Instantiate(henchman, henchmanSpawnPoints[0].transform.position, Quaternion.identity);
        Instantiate(henchman, henchmanSpawnPoints[1].transform.position, Quaternion.identity);
        Instantiate(henchman, henchmanSpawnPoints[2].transform.position, Quaternion.identity);
        Instantiate(henchman, henchmanSpawnPoints[3].transform.position, Quaternion.identity);
        leftHenchman = 4;

        // 배리어 생성

    }

    // 
    public void henchmanKilled()
    {
        leftHenchman--;

        // 하수인이 모두 죽는다면 배리어가 파괴됨
        if(leftHenchman == 0)
        {

        }
    }
}
