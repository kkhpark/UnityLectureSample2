using UnityEngine;

public class TouchInteraction : MonoBehaviour
{
    private GameObject Obj;

    //Object Move
    //Object Rotate

    public float DragScale = 1.1f;

    public float touchTimeLimit = 0.28f;

    private bool isTouched;
    private bool isRotated;
    private float touchTime;


    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit; //충돌한 객체의 정보를 담는 객체

            if (Physics.Raycast(ray, out hit) && !isTouched) //맞추면 한번만 담아
            {
                Obj = hit.transform.gameObject;
                isTouched = true;
            }

            //Enter Rotate Mode
            if (touch.phase == TouchPhase.Stationary)
            {
                if (touchTime >= touchTimeLimit)
                {
                    if (!isRotated)
                    {
                        Obj.transform.localScale *= DragScale;
                    }
                    touchTime = touchTimeLimit;
                    isRotated = true;
                }
                else
                {
                    touchTime += Time.deltaTime; // frame 지난 시간 인가 봄.
                }
            }

            if (touch.phase == TouchPhase.Moved && isTouched)
            {
                if (isRotated)
                {
                    if (touchTime >= touchTimeLimit)
                    {
                        Obj.transform.Rotate(new Vector3(touch.deltaPosition.y * Mathf.Deg2Rad * 20,
                            -touch.deltaPosition.x * Mathf.Deg2Rad * 20, 0), Space.World);
                        //space world coordiante 를 기준으로 터치 y의 델타를 기준으로 X축 기준으로 이동을 시켜줘야함.
                        //Unity Editor에서 큐브가 정면을 바라보게 한 다음에 y방향으로 손가락을 쓸어 올린다고 가정하면 rotation이 x축으로 일어나는 것을 볼 수 있음.
                        //두번째에 -가 들어가는 이유도 유니티에서 직접 바라보게 하고 해보면 -를 안넣으면 손가락 방향과 반대로 돌아감.

                        //Deg2Rad는 
                        //Radian으로 바꾸어 주는 것.


                    }
                }
            }


            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (Obj != null)
                {
                    touchTime = 0;
                    isTouched = false;
                    if (!isRotated)
                    {
                        isRotated = false;
                        Obj.transform.localScale /= DragScale;
                    }
                    Obj = null;
                }

            }




        }

        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition; //android 에도 deltaPosition이 저장이 되어 있으면 편리할듯..
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude; //magnitude..? 크기라는데
            float touchDelta = (touch0.position - touch1.position).magnitude;

            float zoomDelta = prevTouchDelta - touchDelta; //+면 줄인것, -면 늘린것

            Ray ray = Camera.main.ScreenPointToRay(touch0.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Obj = hit.transform.gameObject;
                Obj.transform.localScale = new Vector3(
                    Obj.transform.localScale.x + zoomDelta,
                    Obj.transform.localScale.y + zoomDelta,
                    Obj.transform.localScale.z);
            }
        }

    }
}
