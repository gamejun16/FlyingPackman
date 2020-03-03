using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public ControllerTracker controllerTracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(getLeftPos(), 0, 0);
        Debug.Log(controllerTracker.left);
    }

    int getLeftPos()
    {
        // 0~640 to -10~10
        // 32로 나누면 0~20 (0~19, 20의 비중은 아주 낮음)
        return (controllerTracker.left / 32) - 10;
    }
}
