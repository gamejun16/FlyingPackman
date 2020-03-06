using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDropper: MonoBehaviour
{
    public int speed;

    /*****
     * 
     * ObjectGenerator.cs에 의해 생성된 Object들을 아래로 이동시키는 역할을 맡는 스크립트
     * 
     * */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 아래로~ 아래로~
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
