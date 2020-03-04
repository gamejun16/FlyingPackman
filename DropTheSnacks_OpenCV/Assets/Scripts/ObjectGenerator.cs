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


    // 생성 지점
    public List<Transform> GeneratePoints;

    // 오브젝트 목록
    public List<GameObject> Objects;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1f)
        {
            timer = 0f;
            int rand = Random.Range(0, GeneratePoints.Count);
            Instantiate(Objects[Random.Range(0, 2)], GeneratePoints[rand].position, Quaternion.identity);
            
        }
    }
}
