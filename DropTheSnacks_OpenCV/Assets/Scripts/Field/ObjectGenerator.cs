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
    float __coinGenerateTerm__ = 1.0f;

    // 생성 지점
    public List<Transform> GeneratePoints;

    // 오브젝트 목록
    public List<GameObject> Objects;

    // 크리쳐 목록
    public List<GameObject> Creatures;


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
        StartCoroutine("coinGenerator");
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
                Instantiate(Creatures[0], GeneratePoints[rand].position, Quaternion.identity);
            }

            yield return null;
        }
    }

    IEnumerator coinGenerator()
    {
        float timer = 0f;
        // 정해진 텀을 따라 계속 코인을 생성
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > __coinGenerateTerm__)
            {
                timer = 0f;

                int rand = Random.Range(0, GeneratePoints.Count);

                int obj;
                if (Random.Range(0, 10) > 0) obj = 0; // coin
                else obj = 1; // heart

                Instantiate(Objects[obj], GeneratePoints[rand].position, Quaternion.identity);
            }

            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
        }
    }


}
