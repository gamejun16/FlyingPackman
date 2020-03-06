using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenchmanController : MonoBehaviour
{
    /****
     * 
     * 보스를 공격하기 위해 제거해야하는 하수인을 컨트롤하는 스크립트
     * 
     * 스스로 파괴될 때까지 미사일을 투하한다?
     * 
     * */

    public GameObject missile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("missileDropper");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator missileDropper()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > 1.0f)
            {
                timer = 0f;

                Instantiate(missile, transform.position, Quaternion.identity);
            }
            yield return null;
        }
    }

}
