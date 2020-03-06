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

    // 생성 지점
    public List<Transform> GeneratePoints;

    // 오브젝트 목록
    public List<GameObject> Items;

    // 크리쳐 목록
    public List<GameObject> Creatures;

    // 위험 물체(Danger)
    // Dangers[0] : 위험물체
    // Dangers[1] : 낙하 지점을 표시하는 화살표
    public List<GameObject> Dangers;

    // Start is called before the first frame update
    void Start()
    {
        stageManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 스테이지의 시작과 정지를 관리하는 스크립트
    void stageManager(bool isPause = false)
    {
        StartCoroutine("creatureGenerator");
        StartCoroutine("itemGenerator");
        StartCoroutine("dangerGenerator");
    }

    IEnumerator itemGenerator()
    {
        float timer = 0f;
        // 정해진 텀을 따라 계속 아이템을 생성
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > __itemGenerateTerm__)
            {
                timer = 0f;

                int rand = Random.Range(0, GeneratePoints.Count);

                int idx;
                if (Random.Range(0, 10) > 0) idx = 0; // coin
                else idx = 1; // heart

                Instantiate(Items[idx], GeneratePoints[rand].position, Quaternion.identity);
            }

            yield return null;
        }
    }

    IEnumerator creatureGenerator()
    {
        float timer = 0f;
        // 정해진 텀을 따라 계속 몬스터를 생성
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > __creatureGenerateTerm__)
            {
                timer = 0f;

                int rand = Random.Range(0, GeneratePoints.Count);

                int idx;
                idx = Random.Range(0, Creatures.Count);

                Instantiate(Creatures[idx], GeneratePoints[rand].position, Quaternion.identity);
            }

            yield return null;
        }
    }
    
    IEnumerator dangerGenerator()
    {
        float timer = 0f;
        while (true)
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


    // 플레이어가 발사한 총알이 화면 상단으로 벗어났을 때 삭제하기위한 구문
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
        }
    }


}
