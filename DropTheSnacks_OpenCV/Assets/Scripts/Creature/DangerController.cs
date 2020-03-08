using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerController : MonoBehaviour
{
    /*****
     * 
     * 위험체의 낙하시 소리를 제어하는 스크립트
     * 
     * */

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundManager.playCreatureSound((int)SoundManager.creature.DANGER_DROP, audioSource);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyArea"))
        {
            Destroy(gameObject);
        }
    }
}
