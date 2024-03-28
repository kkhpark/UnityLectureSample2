using UnityEngine;

public class TouchInteractionProf : MonoBehaviour
{
    private GameObject Obj;

    //Object move
    private Vector3 MovePos, Offset;
    bool isDrag;

    //Object Rotate
    public float DragScale = 1.1f;

    public float touchTimeLimit = 0.28f;

    private bool isTouched;
    private bool isRotated;
    private float touchTime;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("touchCount : " + Input.touchCount);
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && !isTouched)
            {
                Obj = hit.transform.gameObject;
                isTouched = true;
            }

            if (touch.phase == TouchPhase.Began)
            {
                MovePos = new Vector3(touch.position.x, touch.position.y,
                Obj.transform.position.z - Camera.main.transform.position.z);
                Offset = Obj.transform.position - Camera.main.ScreenToWorldPoint(MovePos);
                isDrag = true;
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
                    isDrag = false;
                }
                else
                {
                    touchTime += Time.deltaTime;
                }
            }

            if (touch.phase == TouchPhase.Moved && isTouched)
            {
                //rotate
                if (isRotated)
                {
                    if (touchTime >= touchTimeLimit)
                    {
                        Obj.transform.Rotate(new Vector3(touch.deltaPosition.y * Mathf.Deg2Rad * 20,
                        -touch.deltaPosition.x * Mathf.Deg2Rad * 20, 0), Space.World);
                    }
                }
                //move
                else if (isDrag)
                {
                    MovePos = new Vector3(touch.position.x, touch.position.y,
                    Obj.transform.position.z - Camera.main.transform.position.z);
                    MovePos = Camera.main.ScreenToWorldPoint(MovePos);
                    Obj.transform.position = MovePos + Offset;
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (Obj != null)
                {
                    touchTime = 0;
                    isTouched = false;
                    if (isRotated)
                    {
                        isRotated = false;
                        Obj.transform.localScale /= DragScale;
                    }
                    Obj = null;
                    isDrag = false;
                }
            }
        }

        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDelta = (touch0.position - touch1.position).magnitude;

            float zoomDelta = prevTouchDelta - touchDelta;

            Ray ray = Camera.main.ScreenPointToRay(touch0.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Obj = hit.transform.gameObject;
                Obj.transform.localScale = new Vector3(Obj.transform.localScale.x + zoomDelta * -0.01f,
                Obj.transform.localScale.y + zoomDelta * -0.01f,
                Obj.transform.localScale.z + zoomDelta * -0.01f);
            }
        }
    }
}