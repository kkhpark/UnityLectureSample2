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
        //z�� �׳� �ɵ� �ΰ��� 0�̸� ī�޶� �ٷ� ���̴� �Ⱥ��̴ϱ�.

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
        positionCount = 2; // line renderer�� �⺻ point count (0,1 �̷��� �ΰ� ��� ���� �ʱ갪��)  �Ѱ��ϸ� ������.
        GameObject Line = new GameObject("Line");
        LineRenderer LineRend = Line.AddComponent<LineRenderer>(); // �������� ������Ʈ�� ���� ������Ʈ�� �߰�..?
        Line.transform.parent = cam.transform; // ī�޶��� �ڽ� ������Ʈ�� ��.
        Line.transform.position = mouse; // ��ġ�� ���콺 ��ġ�� ������.

        LineRend.startWidth = 0.01f;
        LineRend.endWidth = 0.01f;
        LineRend.numCornerVertices = 5;
        LineRend.numCapVertices = 5;
        LineRend.material = defaultMaterial;
        LineRend.SetPosition(0, mouse);
        LineRend.SetPosition(1, mouse); // ����Ʈ 2����.


        curLine = LineRend;
    }

    void ConnectLine(Vector3 mouse)
    {
        // �ʹ� ���� �׷����� �ȵǴϱ� threshold�� �ִ� ��.
        if (prevpos != null && Mathf.Abs(Vector3.Distance(prevpos, mouse)) >= 0.01f)
        {
            prevpos = mouse;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mouse);
        }
    }
}
