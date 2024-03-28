using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            RaycastHit hit; //�浹�� ��ü�� ������ ��� ��ü

            if (Physics.Raycast(ray, out hit) && !isTouched) //���߸� �ѹ��� ���
            {
                Obj = hit.transform.gameObject;
                isTouched = true;
            }
            
            //Enter Rotate Mode
            if(touch.phase == TouchPhase.Stationary)
            {
                if(touchTime >= touchTimeLimit)
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
                    touchTime += Time.deltaTime; // frame ���� �ð� �ΰ� ��.
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
                        //space world coordiante �� �������� ��ġ y�� ��Ÿ�� �������� X�� �������� �̵��� ���������.
                        //Unity Editor���� ť�갡 ������ �ٶ󺸰� �� ������ y�������� �հ����� ���� �ø��ٰ� �����ϸ� rotation�� x������ �Ͼ�� ���� �� �� ����.
                        //�ι�°�� -�� ���� ������ ����Ƽ���� ���� �ٶ󺸰� �ϰ� �غ��� -�� �ȳ����� �հ��� ����� �ݴ�� ���ư�.

                        //Deg2Rad�� 
                        //Radian���� �ٲپ� �ִ� ��.


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
        
    }
}
