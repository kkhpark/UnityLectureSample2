using UnityEngine;
using UnityEngine.UI;

public class TouchExample : MonoBehaviour
{
    public Text txt;
    // input�� ������ �������ε� touch�� mobile device only
    // Game â������ Mouse input�� ���� �־ touch�� �ȵ�.


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
                Ray ray = Camera.main.ScreenPointToRay(touch.position); //��ġ �����ǿ��� �����°� ray�� ����
                //�� ��ǥ�� x,y�ε� �� ��ġ�� ������� �߻��ϴ� �����̴� ��°�

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Obj = hit.transform.gameObject;
                    MovePos = new Vector3(touch.position.x, touch.position.y,
                        transform.position.z - Camera.main.transform.position.z); //��ü�� ī�޶� ������ �Ÿ�
                    Offset = Obj.transform.position - Camera.main.ScreenToWorldPoint(MovePos); 
                    isDrag = true;
                }
            }
            if (touch.phase == TouchPhase.Moved && isDrag)
            {
                txt.text = "Touch Moved";
                MovePos = new Vector3(touch.position.x, touch.position.y,
                       transform.position.z - Camera.main.transform.position.z); //��ü�� ī�޶� ������ �Ÿ�
                MovePos = Camera.main.ScreenToWorldPoint(MovePos); // ��ġ �������� �ٽ� ���� ���������� �ٲپ ������
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
