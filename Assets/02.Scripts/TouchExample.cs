using UnityEngine;
using UnityEngine.UI;

public class TouchExample : MonoBehaviour
{
    public Text txt;
    // input의 종류가 여러개인데 touch는 mobile device only
    // Game 창에서는 Mouse input이 따로 있어서 touch는 안됨.


    private GameObject Obj;
    private Vector3 MovePos, Offset;
    bool isDrag;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // first touch information
            if (touch.phase == TouchPhase.Began)
            {
                txt.text = "Touch Began";
                Ray ray = Camera.main.ScreenPointToRay(touch.position); //터치 포지션에서 나가는걸 ray로 변경
                //그 좌표가 x,y인데 그 위치를 기반으로 발사하는 광선이다 라는거

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Obj = hit.transform.gameObject;
                    MovePos = new Vector3(touch.position.x, touch.position.y,
                        transform.position.z - Camera.main.transform.position.z); //객체와 카메라 사이의 거리
                    Offset = Obj.transform.position - Camera.main.ScreenToWorldPoint(MovePos); 
                    isDrag = true;
                }
            }
            if (touch.phase == TouchPhase.Moved && isDrag)
            {
                txt.text = "Touch Moved";
                MovePos = new Vector3(touch.position.x, touch.position.y,
                       transform.position.z - Camera.main.transform.position.z); //객체와 카메라 사이의 거리
                MovePos = Camera.main.ScreenToWorldPoint(MovePos); // 터치 포지션을 다시 월드 포지션으로 바꾸어서 재저장
                Obj.transform.position = MovePos + Offset;
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                txt.text = "Touch Stationary";
            }

            if (touch.phase == TouchPhase.Ended)
            {
                txt.text = "Touch Ended";
                Obj = null;
                isDrag = false;

            }


            if (touch.phase == TouchPhase.Canceled)
            {
                txt.text = "Touch Canceled";
            }
        }

    }
}
