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

    // Start is called before the first frame update
    void Start()
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
}
