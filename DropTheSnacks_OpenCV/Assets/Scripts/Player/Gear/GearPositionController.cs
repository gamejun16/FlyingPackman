using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPositionController : MonoBehaviour
{
    /*****
     * 
     * Gear들을 생성하고 플레이어 주변에 위치하게끔 관리하는 스크립트
     * 
     * 최대 10개의 슬롯이 존재하며 플레이어를 살짝 늦게 따라다닌다.
     * 
     * 신규 기어를 장착하고 업그레이드하는 함수를 제공한다.
     * 
     * */

    // 장착할 수 있는 기어 목록
    public List<GameObject> gear;

    // 따라다닐 플레이어
    public GameObject player;

    // Gear들이 위치할 수 있는 슬롯
    public List<GameObject> slot;

    // 다음 번째 기어를 위치시킬 수 있는 인덱스 값
    // 0~9 : new gear
    // 10~19 : gear upgrade from 0(level) to 1
    // 20~29 : gear upgrade from 1 to 2(max)
    int idx;

    // Start is called before the first frame update
    void Start()
    {
        initParameters();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((player.transform.position.x - transform.position.x) / 10, 0, 0, Space.World);
    }

    void initParameters()
    {
        idx = 0;
    }

    // 신규 기어를 장착하는 함수
    public void newGear(int gearNumb = 0)
    {
        if (idx / slot.Count == 0)
        {
            Instantiate(gear[gearNumb], slot[idx].transform);
            idx++;
        }
        else if(idx / slot.Count == 1 || idx/slot.Count == 2)
        {
            slot[idx % slot.Count].GetComponentInChildren<GearController>().gearUpgrade();
            idx++;
        }
    }
}
