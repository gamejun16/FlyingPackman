using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 컨트롤러로 조작되는 고스트 플레이어
    public GameObject ghost;

    // 자식 오브젝트 레이저
    public GameObject Laser;

    public AudioSource laserAudioSource;

    private void Awake()
    {
        laserAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LaserSetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        move();

    }

    // 플레이어의 움직임(이동 및 회전)을 담당
    void move()
    {
        // -5 ~ 5 to 
        float angle = (transform.position.x - ghost.transform.position.x) * 15f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 매 Update마다 현 위치와 Ghost 위치의 중앙으로 이동(수렴)
        //transform.position = new Vector3((transform.position.x + ghost.transform.position.x) / 2, -4, 0);
        transform.Translate((ghost.transform.position.x - transform.position.x) / 10, 0, 0, Space.World);
    }

    // Laser(GameObject)를 키고 끄는 함수
    public void LaserSetActive(bool On = false)
    {
        // sound
        if (On) SoundManager.soundManager.playOtherSound((int)SoundManager.other.LASER, laserAudioSource);
        else laserAudioSource.Stop();

        Laser.SetActive(On);
    }

}
