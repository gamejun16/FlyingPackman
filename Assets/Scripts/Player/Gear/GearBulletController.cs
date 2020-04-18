using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBulletController : MonoBehaviour
{
    /*****
     * 
     * Gear에서 발사된 총알을 움직이는 스크립트
     * 
     * */

    public int speed;

    // 0 - 1 - 2 단계
    public int upgrade;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine("shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator shoot()
    {
        while (true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);

            yield return null;
        }
    }



    // 플레이어가 발사한 총알이 화면 상단으로 벗어났을 때 삭제하기위한 구문
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // cannotEat. creature, boss etc.
        if(collision.gameObject.layer == 9)
        {
            PoolingManager.poolingManager.returnBullet(gameObject, upgrade);
        }
        else  if (collision.CompareTag("GenerateArea"))
        {
            PoolingManager.poolingManager.returnBullet(gameObject, upgrade);
        }
    }
}
