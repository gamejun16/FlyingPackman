using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    /*****
     * 
     * 영상을 통해 인식된 컨트롤러의 위치를 기반으로 이동한다
     * CharacterController를 통해 Ghost를 따라다니며 게임이 진행된다?
     * 
     * */

    public ControllerTracker controllerTracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(getLeftPos(), 0, 0);
        //Debug.Log(controllerTracker.left);
    }

    float getLeftPos()
    {
        // 0~640 to -10~10
        // 32로 나누면 0~20 (0~19, 20의 비중은 아주 낮음)
        return ((controllerTracker.center / 32f) - 10) * -1;
    }
}
