using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class QRObjectPlacement : MonoBehaviour
{
    public static QRObjectPlacement Instance;
    public string qrcode;


    ARRaycastManager arRaycastManager; //raycast for ar
    static List<ARRaycastHit> hits = new List<ARRaycastHit>(); // raycasts means just ray is collided or not

    public Camera ARCam;
    public GameObject ARIndicator;

    public GameObject[] ObjectToSpawn;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>(); // input value to arRaycastManager... where that come from?
    }

    // Update is called once per frame
    void Update()
    {
        if (qrcode != "")
        {
            PlaneIndication();
        }
    }

    private void PlaneIndication()
    {
        //Get center point of mobile screen
        var screenCenter = ARCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        //ui screen - world 가 분리 

        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.All)) // raycast from the screenCenter
        {
            Pose hitPos = hits[0].pose; //pose struct  충돌 정보 중에 pose를 hitpose 에 저장 (위치 값) (from the first hit object)
            //save hit location value to hitpose

            var cameraForward = ARCam.transform.forward; // camera가 바라보고 있는 방향 camera lookat direction (forward means +z direction)
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized; //camera forward x,z and normalized means unit vector
            // why unit vector?  indicator should be parallel to the surface , so that is why y is zero..??
            // 

            hitPos.rotation = Quaternion.LookRotation(cameraBearing);

            ARIndicator.SetActive(true);
            ARIndicator.transform.SetPositionAndRotation(hitPos.position, hitPos.rotation);

            SpawnObject(hitPos);
        }
        else
        {
            ARIndicator.SetActive(false);
        }
    }

    private void SpawnObject(Pose hitPos)
    {
        if (Input.touchCount > 0) // whether screen is touched or not
        {
            if (qrcode == "acorn")
            {
                GameObject obj = Instantiate(ObjectToSpawn[0], hitPos.position, hitPos.rotation);
            } else if (qrcode == "airplane")
            {
                GameObject obj = Instantiate(ObjectToSpawn[1], hitPos.position, hitPos.rotation);
            } else/* if (qrcode == "apple")*/
            {
                GameObject obj = Instantiate(ObjectToSpawn[2], hitPos.position, hitPos.rotation);
            }
        }
    }
}
