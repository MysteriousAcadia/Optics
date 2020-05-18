using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    //Variables for line object
    PencilLine pencilLine;
    protected LineRenderer line;
    public Vector3 startPos, endPos;
    float mZCoordinate;
    public GameObject currentLine, lineObject;
    public Material dottedLine;
    public Prism prism;
    List<Vector3> prismVertices = new List<Vector3>();
    List<LineEquation> prismLineEquations = new List<LineEquation>();
    bool isDrawEnabled = false;

    public UIManager uIManager;
    public GameObject upperDeckPencil,upperDeckProtractor;

    // Start is called before the first frame update
    void Start()
    {
        isDrawEnabled = false;


    }
    public void ActivateDrawLine(){
        isDrawEnabled = true;
        uIManager.UpdateDecks(upperDeckPencil,2);
    }

    public GameObject drawLine(LineEquation l)
    {
        currentLine = Instantiate(lineObject);
        pencilLine = currentLine.GetComponent<PencilLine>();
        currentLine.transform.SetParent(gameObject.transform);
        currentLine.transform.localPosition = new Vector3(100, 110, 110);
        currentLine.transform.position = gameObject.transform.position;
        line = currentLine.GetComponent<LineRenderer>();
        line.material = dottedLine;
        // line.material.color = Color.green;
        l.Point1.z = 0.1f;
        l.Point2.z = 0.1f;
        line.SetPosition(0, l.Point1);
        line.SetPosition(1, l.Point2);
        currentLine.GetComponent<PencilLine>().AddColliderToLine();
        return currentLine;
    }

    void OnMouseDown()
    {
        if(!isDrawEnabled){
            return;
        }
        mZCoordinate = gameObject.transform.position.z;
        prismLineEquations = new List<LineEquation>();
        List<Vector3> prismVertices = prism.getVertices();
        LineEquation l1 = new LineEquation(prismVertices[0], prismVertices[1]);
        // l1.setConstants(prismVertices[0],prismVertices[1]);
        LineEquation l2 = new LineEquation(prismVertices[1], prismVertices[2]);
        // l2.setConstants(prismVertices[1],prismVertices[2]);
        LineEquation l3 = new LineEquation(prismVertices[0], prismVertices[2]);
        // l3.setConstants(prismVertices[0],prismVertices[2]);
        prismLineEquations.Add(l1);
        prismLineEquations.Add(l2);
        prismLineEquations.Add(l3);
        currentLine = Instantiate(lineObject);
        pencilLine = currentLine.GetComponent<PencilLine>();
        currentLine.transform.SetParent(gameObject.transform);
        currentLine.transform.localPosition = new Vector3(0, 0, 0);
        currentLine.transform.position = gameObject.transform.position;
        line = currentLine.GetComponent<LineRenderer>();

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == gameObject.transform.gameObject.name)
            {
                foreach (LineEquation l in prismLineEquations)
                {
                    if (l.perpDistance(new Vector3(hit.point.x, hit.point.y, 0f)) < 0.1f)
                    {
                        Debug.LogError("COULD BE SNAPPED");
                    }
                }
                pencilLine.vertices.Add(new Vector3(hit.point.x, hit.point.y, 0.1f));

                line.SetPosition(0, new Vector3(hit.point.x, hit.point.y, 0.1f));
                line.SetPosition(1, hit.point);

            }
        }

    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePos);

    }

    void OnMouseDrag()
    {
        if (!isDrawEnabled)
        {
            return;
        }
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == gameObject.transform.gameObject.name)
            {

                line.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 0.1f));
                pencilLine.vertices[1] = hit.point;
            }

        }

    }
    void OnMouseUp()
    {
        if (!isDrawEnabled)
        {
            return;
        }
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == gameObject.transform.gameObject.name)
            {
                bool hasSnapped = false;
                foreach (LineEquation l in prismLineEquations)
                {
                    if (l.perpDistance(new Vector3(hit.point.x, hit.point.y, hit.point.z)) < 0.5f)
                    {

                        Vector2 point = l.GetClosestPointOnLineSegment(new Vector2(hit.point.x, hit.point.y));
                        LineEquation l1 = new LineEquation(pencilLine.vertices[0], point);
                        // l1.setConstants(pencilLine.vertices[0],point);
                        Debug.LogError("Inputs Given: line1:" + l.ToString() + ", line2:" + l1.ToString());
                        Debug.LogError("Prism Line Equation 1:" + prismLineEquations[0].ToString());
                        Debug.LogError("Prism Line Equation 2:" + prismLineEquations[1].ToString());
                        Debug.LogError("Prism Line Equation 3:" + prismLineEquations[2].ToString());
                        line.SetPosition(1, new Vector3(point.x, point.y, hit.point.z + 0.1f));
                        prism.Calculation(l, l1, prismLineEquations);
                        isDrawEnabled = false;
                        // uIManager.GoBack();
                        // pencilLine.AddColliderToLine();

                        // prism.Calculation(l1,l,prismLineEquations);

                        hasSnapped = true;
                    }
                }
                if (!hasSnapped)
                {
                    line.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 0f));
                    line.SetPosition(1, hit.point);
                    pencilLine.AddColliderToLine();
                }

            }
            isDrawEnabled = false;
            uIManager.GoBack();

        }
    }
    LineEquation l1,l2;
    bool measuringAngles = false;
  public void Measure2Angles()
  {
      uIManager.UpdateDecks(upperDeckProtractor,6);
      measuringAngles = true;
      l1 = null;
      l2=null;
    
  }
    void Update()
    {
        if (measuringAngles)
        {
            if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.collider != null)
                    {
                        GameObject touchedObject = hit.transform.gameObject;

                        Debug.Log("Touched " + touchedObject.transform.name);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Casts the ray and get the first game object hit
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("This hit at " + hit.point);
                    if (hit.collider != null)
                    {
                        GameObject touchedObject = hit.transform.gameObject;
                        if (touchedObject.transform.name == "PencilLine(Clone)")
                        {
                            if(l1==null)
                            {
                                l1 = new LineEquation(touchedObject.GetComponent<LineRenderer>().GetPosition(0),
                                    touchedObject.GetComponent<LineRenderer>().GetPosition(1));
                            }
                            else
                            {
                                l2 = new LineEquation(touchedObject.GetComponent<LineRenderer>().GetPosition(0),
                                    touchedObject.GetComponent<LineRenderer>().GetPosition(1));
                                measuringAngles = false;
                                Debug.LogError(l1.angleBetween(l2));
                                uIManager.GoBack();

                            }
                        }
                        Debug.Log("Touched " + touchedObject.transform.name);
                    }

                }

            }
        }
    }
}

