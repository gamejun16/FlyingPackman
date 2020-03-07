using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    /*****
     * 
     * 각 Gear를 컨트롤하는 스크립트
     * 
     * 간격에 맞춰 총알을 쏜다
     * 
     * */

    public List<GameObject> bullet;
    public float shootTerm; // 연사 텀

    public int upgrade; // 0~2 단계 강화 수준

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("bulletInstantiater");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator bulletInstantiater()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > shootTerm)
            {
                timer = 0f;

                //Instantiate(bullet[upgrade], transform.position, Quaternion.identity);
                GameObject bullet = PoolingManager.poolingManager.getBullet(upgrade);
                bullet.transform.position = transform.position;
            }
            
            yield return null;
        }
        
    }

    // 기어를 강화하는 함수
    public void gearUpgrade()
    {
        if (upgrade < 2)
        {
            // 총알 변경
            upgrade++;

            // 연사 속도 증가
            shootTerm -= 0.1f;
        }
    }

    
}
