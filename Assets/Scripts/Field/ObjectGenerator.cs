using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    /****
     * 
     * 화면 위에서 내려올 오브젝트를 생성하는 스크립트
     * 
     * */

    float __creatureGenerateTerm__ = 0.5f;
    float __itemGenerateTerm__ = 1.0f;
    float __dangerGenerateTerm__ = 5.0f;

    // 코루틴을 안전하게 종료하기위한 플래그
    bool coroutineLoopFlag;

    // 생성 지점
    public List<Transform> GeneratePoints;

    // 오브젝트 목록
    public List<GameObject> Items;

    // 크리쳐 목록
    public List<GameObject> Creatures;

    // 보스 목록
    public List<GameObject> Bosses;

    // 위험 물체(Danger)
    // Dangers[0] : 위험물체
    // Dangers[1] : 낙하 지점을 표시하는 화살표
    public List<GameObject> Dangers;

    // Start is called before the first frame update
    void Start()
    {
        stageStarter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 스테이지의 시작과 정지를 관리
    // 파라미터로 현 스테이지 정보를 넘겨주어 난이도를 조정?
    void stageStarter(int curStage = 1)
    {
        coroutineLoopFlag = true;
        StartCoroutine("creatureGenerator");
        StartCoroutine("itemGenerator");
        StartCoroutine("dangerGenerator");
    }
    
    IEnumerator itemGenerator()
    {
        float timer = 0f;
        // 정해진 텀을 따라 계속 아이템을 생성
        while (coroutineLoopFlag)
        {
            timer += Time.deltaTime;
            if (timer > __itemGenerateTerm__)
            {
                timer = 0f;

                int rand = Random.Range(0, GeneratePoints.Count);

                int idx;
                if (Random.Range(0, 10) > 0) idx = 0; // coin
                else idx = 1; // heart

                //Instantiate(Items[idx], GeneratePoints[rand].position, Quaternion.identity);
                GameObject obj = PoolingManager.poolingManager.getItem(idx);
                obj.transform.position = GeneratePoints[rand].position;
            }

            yield return null;
        }
    }

    IEnumerator creatureGenerator()
    {
        float timer = 0f;
        // 정해진 텀을 따라 계속 몬스터를 생성
        while (coroutineLoopFlag)
        {
            timer += Time.deltaTime;
            if(timer > __creatureGenerateTerm__)
            {
                timer = 0f;

                int rand = Random.Range(0, GeneratePoints.Count);

                //Instantiate(Creatures[0], GeneratePoints[rand].position, Quaternion.identity);
                GameObject obj = PoolingManager.poolingManager.getCreature(0);
                obj.transform.position = GeneratePoints[rand].position;
            }

            yield return null;
        }
    }

    IEnumerator dangerGenerator()
    {
        float timer = 0f;
        while (coroutineLoopFlag)
        {
            timer += Time.deltaTime;

            // 위험 물체 발사 준비
            if (timer > __dangerGenerateTerm__)
            {
                timer = 0f;

                int rand = Random.Range(0, GeneratePoints.Count);

                // 낙하 위치 표시
                Vector3 pointInstantiatsPos = GeneratePoints[rand].position;
                pointInstantiatsPos.y -= 10;

                GameObject point = Instantiate(Dangers[1], pointInstantiatsPos, Quaternion.identity);

                // 낙하 위치 경고 1초 후 위험체 발사
                while (timer < 1f)
                {
                    timer += Time.deltaTime;

                    point.transform.Translate((GeneratePoints[rand].position - point.transform.position) / 5);


                    yield return null;
                }
                // 화살표 삭제
                Destroy(point);

                // 위험체 발사
                Instantiate(Dangers[0], GeneratePoints[rand].position, Quaternion.identity);
            }

            yield return null;
        }
    }

    // 코루틴을 안전하게 종료시키는 함수
    void stopAllCoroutine()
    {
        coroutineLoopFlag = false;
    }
    

    // 해당 스테이지의 보스 전투를 시작
    public void bossStageStarter(int stage)
    {
        stopAllCoroutine();
        //StopCoroutine("creatureGenerator");
        //StopCoroutine("itemGenerator");
        //StopCoroutine("dangerGenerator");

        // 보스 위치로
        Instantiate(Bosses[stage - 1], GeneratePoints[(int)(GeneratePoints.Count * 0.5)].transform.position, Quaternion.identity);
    }

}
