using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

using System.Threading;

public class ControllerTracker : MonoBehaviour
{

    [DllImport("controllerTrackingDLL")]
    private static extern bool cv_TrackingOn();

    [DllImport("controllerTrackingDLL")]
    private static extern bool cv_TrackingOff();

    [DllImport("controllerTrackingDLL")]
    private static extern bool cv_Tracking(ref int _center);

    // 영상 인식 및 처리 과정을 별도의 스레드로 분리
    Thread thread;

    // 개발 환경 랩탑의 경우 640*480 웹캠이기에 0~640까지 인식
    public int center;


    bool threadIsOn;

    // Start is called before the first frame update
    void Start()
    {
        if (thread == null)
        {
            // 카메라 준비
            if (!TrackingOn())
            {
                Debug.Log("Tracking On Error !!");
            }
            else
            {
                Debug.Log("Tracking On Success !!");
                TrackingStart();
            }
            // 스레드 생성 및 Start
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 생명 주기. 프로그램이 종료되는 시점에 호출. 에디터 동작X
    private void OnApplicationQuit()
    {
        //thread.Abort(); // 스레드 종료. 원활한 종료가 이루어지지 않을 수 있음?
        Debug.Log("[QUIT IS CALL]");

        TrackingOff();
    }

    // 트래킹 시작 준비
    public bool TrackingOn()
    {
        if (!cv_TrackingOn())
        {
            Debug.Log("(-1) cam on fail");

            return false;
        }
        Debug.Log("(1) cam on");
        return true;
    }
    // 트래킹(thread) 시작
    public void TrackingStart()
    {
        thread = new Thread(ThreadTracking);
     
        threadIsOn = true; //스레드 중지를 위한 플래그

        thread.Start();

        Debug.Log("(2) thread start");
    }

    // 트래킹(카메라) 완전 종료
    public bool TrackingOff()
    {
        // 스레드를 가동케 하던 루프의 작동을 중단시킴
        threadIsOn = false;

        if (!cv_TrackingOff())
        {
            Debug.Log("cam off fail");
            return false;
        }

        Debug.Log("(4) cam off");

        // 스레드가 완전히 정지할 때까지 대기

        if (thread != null)
        {
            thread.Interrupt();
            thread.Join();

            thread = null;
        }

        
        Debug.Log("(5) thread off");

        // 스레드 종료 완료?


        return true;
    }

    void ThreadTracking()
    {

        while (threadIsOn)
        {
            bool tmp = cv_Tracking(ref center);
            if (!tmp)
            {
                
                Debug.Log("Top Cam Error");
                TrackingOff();

                return;
            }
            else
            {
                //
            }


        }
        Debug.Log("(3) thread Top loop is done");
        return;
    }

}
