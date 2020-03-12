using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    /*****
     * 
     * 오브젝트 풀링을 구현한 스크립트
     * 
     * */


    public static PoolingManager poolingManager;

    // 단계별 총알(0, 1, 2)
    Queue<GameObject>[] bullet= new Queue<GameObject>[3];
    public List<GameObject> bullet_pref;

    // 아이템 종류(coin, hp)
    Queue<GameObject>[] item = new Queue<GameObject>[2];
    public List<GameObject> item_pref;

    // 단계별 크리쳐
    Queue<GameObject>[] creature = new Queue<GameObject>[1];
    public List<GameObject> creature_pref;

    // 특수 무기
    Queue<GameObject>[] specialBullet = new Queue<GameObject>[1];
    public List<GameObject> specialBullet_pref;

    private void Awake()
    {
        poolingManager = this;

        initBullet();
        initItem();
        initCreature();
        initSpecialBullet();
    }

    // 총알 준비
    void initBullet()
    {
        bullet[0] = new Queue<GameObject>();
        bullet[1] = new Queue<GameObject>();
        bullet[2] = new Queue<GameObject>();

        for (int i = 0; i < bullet.Length; i++)
        {
            for (int j = 0; j < 80; j++)
            {
                GameObject obj = Instantiate(bullet_pref[i], this.transform);
                obj.SetActive(false);

                // 생성한 오브젝트 저장
                bullet[i].Enqueue(obj);
            }
        }
    }
    public GameObject getBullet(int upgrade)
    {
        GameObject obj = bullet[upgrade].Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void returnBullet(GameObject obj, int upgrade)
    {
        obj.SetActive(false);
        bullet[upgrade].Enqueue(obj);
    }

    
    // 아이템 준비
    void initItem()
    {
        item[0] = new Queue<GameObject>();
        item[1] = new Queue<GameObject>();

        for(int i = 0; i < item.Length; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                GameObject obj = Instantiate(item_pref[i], this.transform);
                obj.SetActive(false);

                // 생성한 오브젝트 저장
                item[i].Enqueue(obj);
            }
        }
    }
    public GameObject getItem(int idx)
    {
        GameObject obj = item[idx].Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void returnItem(GameObject obj, int idx)
    {
        obj.SetActive(false);
        item[idx].Enqueue(obj);
    }
    
    // 크리쳐 준비
    void initCreature()
    {
        creature[0] = new Queue<GameObject>();

        for (int i = 0; i < creature.Length; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                GameObject obj = Instantiate(creature_pref[i], this.transform);
                obj.SetActive(false);

                // 생성한 오브젝트 저장
                creature[i].Enqueue(obj);
            }
        }
    }
    public GameObject getCreature(int idx)
    {
        GameObject obj = creature[idx].Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void returnCreature(GameObject obj, int idx)
    {
        obj.SetActive(false);
        creature[idx].Enqueue(obj);
    }

    // 특수무기 준비
    void initSpecialBullet()
    {
        specialBullet[0] = new Queue<GameObject>();

        for (int i = 0; i < specialBullet.Length; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                GameObject obj = Instantiate(specialBullet_pref[i], this.transform);
                obj.SetActive(false);

                // 생성한 오브젝트 저장
                specialBullet[i].Enqueue(obj);
            }
        }
    }
    public GameObject getSpecialBullet(int idx)
    {
        GameObject obj = specialBullet[idx].Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void returnSpecialBullet(GameObject obj, int idx)
    {
        obj.SetActive(false);
        specialBullet[idx].Enqueue(obj);
    }
}
