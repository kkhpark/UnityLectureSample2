using UnityEngine;

public class Drawing : MonoBehaviour
{
    public Camera cam;
    public Material defaultMaterial;

    private LineRenderer curLine;
    private int positionCount = 2;
    private Vector2 prevpos = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DrawMouse();
    }


    void DrawMouse()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3f));
        //z는 그냥 심도 인가봐 0이면 카메라 바로 앞이니 안보이니까.

        if (Input.GetMouseButtonDown(0)) //left click
        {
            CreateLine(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
            ConnectLine(mousePos);
        }

    }

    void CreateLine(Vector3 mouse)
    {
        positionCount = 2; // line renderer의 기본 point count (0,1 이렇게 두개 들어 있음 초깃값이)  한개하면 에러남.
        GameObject Line = new GameObject("Line");
        LineRenderer LineRend = Line.AddComponent<LineRenderer>(); // 동적으로 오브젝트를 만들어서 컴포넌트를 추가..?
        Line.transform.parent = cam.transform; // 카메라의 자식 오브젝트로 들어감.
        Line.transform.position = mouse; // 위치는 마우스 터치의 포지션.

        LineRend.startWidth = 0.01f;
        LineRend.endWidth = 0.01f;
        LineRend.numCornerVertices = 5;
        LineRend.numCapVertices = 5;
        LineRend.material = defaultMaterial;
        LineRend.SetPosition(0, mouse);
        LineRend.SetPosition(1, mouse); // 디폴트 2개임.


        curLine = LineRend;
    }

    void ConnectLine(Vector3 mouse)
    {
        // 너무 많이 그려지면 안되니까 threshold를 주는 것.
        if (prevpos != null && Mathf.Abs(Vector3.Distance(prevpos, mouse)) >= 0.01f)
        {
            prevpos = mouse;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mouse);
        }
    }
}
