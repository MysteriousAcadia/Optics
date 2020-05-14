// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using System.Linq;

// public class Prism : MonoBehaviour
// {
//     // Start is called before the first frame update
//     [SerializeField]
//     public GameObject board,//Provide reference of the board, used to ensure prism is on the board.
//          pencilLine;//Provide reference to the prefab of pencilLine.
//     [SerializeField]
//     protected float RefractiveIndex = 1.5f; //Refractive Index
//     [SerializeField]
//     protected float a = 60f; //Angle of Prism 
//     bool isSelected = false;//
//     public List<LineEquation> result;

//     public DrawLine drawLine;
//     public void setRotation(float angle){ //Call it to change orientation of prism
//         transform.Rotate(new Vector3(0, angle, 0));
//         Debug.LogError(circleRadius());
//     }
//     void setPosition(Vector3 position){
//         gameObject.transform.position = new Vector3(position.x,position.y,gameObject.transform.position.z);
//         isSelected = false;
//     }

//     public float getRefractiveIndex(){
//         return(RefractiveIndex);
//     }
//     public float getAngleOfPrism(){
//         return(a);
//     }

//     public float circleRadius(){
//         List<Vector3> vertices = getVertices();
//         float a = Vector3.Distance(vertices[0], vertices[1]);
//         float b = Vector3.Distance(vertices[1], vertices[2]);
//         float c = Vector3.Distance(vertices[0], vertices[2]);
//         float r = a*b*c/ (float)Math.Sqrt((a+b-c)*(b+c-a)*(c+a-b)*(a+b+c));
//         return(r);

//     }
    
//     void OnMouseUp() //Scrip to Move prisim, call it when user touches on the prism
//     {
//         if(isSelected){
            
//         }
//         else{
//             isSelected = true;
//         }
//     }
//     public List<Vector3> getVertices(){
//         Vector3[] meshVerts = gameObject.GetComponent<MeshFilter>().mesh.vertices;
//         List<Vector3> vertices = new List<Vector3>();
//         foreach(Vector3 meshVert in meshVerts){
//             Vector3 meshVertx = transform.TransformPoint(meshVert);
//             Debug.LogError(meshVertx);

//             meshVertx.z = 0.2f;
//             vertices.Add(meshVertx);
//             Debug.LogError(meshVertx);

//          }
//          return(vertices);

//     }
//     void Start()
//     {
//         if(!board){
//             board = GameObject.Find("Board");
//         }
//         if(pencilLine && board){
//             Debug.LogError("Prism::One of the GameObject is not linked!");
//         }
//     }
//     void changePosition(){
//         RaycastHit hit;
//             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//             if (Physics.Raycast(ray, out hit))
//             {
//                 if(hit.transform.gameObject.name==board.transform.gameObject.name){ //Checking if the point is on the board.
//                     setPosition(hit.point);
//                 }
                
//             }
//     }


//     // Update is called once per frame
//     void Update()
//     {
//         if(isSelected && Input.GetMouseButtonUp(0)){
//             changePosition();

//         }
//         if(Input.GetKeyUp("space")){
//             drawVertices();
//         }
        
//     }
//     void drawVertices(){
//         List<Vector3> vertices = getVertices();
//         GameObject p1 = Instantiate(pencilLine);
//         p1.transform.SetParent(gameObject.transform);
//         p1.transform.localPosition = new Vector3(0,0,0);
//         p1.transform.position = gameObject.transform.position;
//         p1.GetComponent<PencilLine>().drawLine(vertices[0],vertices[1]);
//         GameObject p2 = Instantiate(pencilLine);
//         p2.transform.SetParent(gameObject.transform);
//         p2.transform.localPosition = new Vector3(0,0,0);
//         p2.transform.position = gameObject.transform.position;
//         p2.GetComponent<PencilLine>().drawLine(vertices[1],vertices[2]);
//         GameObject p3 = Instantiate(pencilLine);
//         p3.transform.SetParent(gameObject.transform);
//         p3.transform.localPosition = new Vector3(0,0,0);
//         p3.transform.position = gameObject.transform.position;
//         p3.GetComponent<PencilLine>().drawLine(vertices[2],vertices[0]);

//     }
//     bool isToggle = false;
//     List<GameObject> lines = new List<GameObject>();

//     public void ToggleView(){
//         if(isToggle&& result.Count()>0){
//             foreach(LineEquation l in result){
//                 lines.Add(drawLine.drawLine(l));
//             }
//         }
//         else if(lines.Count()>0){
//             foreach(GameObject line in lines){
//                 Destroy(line);
//             }
//         }
//         isToggle = !isToggle;

//     }
//     //Function to calculate angle between 2 lines.(wrtNormal is to get the incidentAngle)
// //    public float calculateAngle(List<Vector3> line1, List<Vector3> line2, bool wrtNormal){
// //         float a1 =(float)Math.Atan2(line1[0].y-line1[1].y,line1[0].x-line1[1].x);
// //         float a2 =(float)Math.Atan2(line2[0].y-line2[1].y,line2[0].x-line2[1].x);
// //         float angle = (a2-a1) * (180/(float)Math.PI);
// //         while(!(angle>=0 && angle<=360)){
// //             if(angle<0){
// //                 angle+=360f;
// //             }
// //             else{
// //                 angle-=360f;
// //             }
// //         }
// //         if(angle>180){
// //             angle = 360f-angle;
// //         }
// //         if(wrtNormal){
// //         if(angle>90){
// //             return(angle-90f);
// //         }
// //         else{
// //             return(90f-angle);
// //         }
// //         }
// //         else{
// //             return(angle);
// //         }
// //     }
// //     // line1, line2, and line3 are the equations of line and the planes of the prism
// //     // All intermediate angles are in degrees.
// //     public void calculateImageLine(LineEquation line1,LineEquation line2,List<LineEquation> prismLineEquations){
// //         // float incidentAngle = calculateAngle(line1.getPoints(),line2.getPoints(),true);
// //         Debug.LogError(line1.getConstants().ToString()+" "+line2.getConstants().ToString());
// //         float incidentAngle = 90f-line1.angleBetween(line2);
// //         float refractedAngle1 = radToDeg((float)Math.Asin((float)Math.Sin(degToRad(incidentAngle))/getRefractiveIndex()));
// //         Debug.LogError("RefractedAngle"+refractedAngle1);
// //         LineEquation refractedRay = rotatedLine(-90+refractedAngle1,line1,line2.Point2);//works
// //         // LineEquation refractedRay = rotatedLine(refractedAngle1-incidentAngle,line2,line2.pointOfIntersection(line1));
// //         LineEquation refractedRaytry = rotatedLine(-refractedAngle1+incidentAngle,line2,line2.pointOfIntersection(line1));
// //         LineEquation normal1 = rotatedLine(90f,line1,line1.pointOfIntersection(line2));
// //         if(refractedRaytry.angleBetween(normal1)<refractedRay.angleBetween(normal1)){
// //             refractedRay = refractedRaytry;
// //         }

        
// //         int minPos = 0;
// //         float minDist = 10000f;
// //         int i = 0;
// //         foreach(LineEquation l in prismLineEquations){
// //             float dist = line1.perpDistance(refractedRay.pointOfIntersection(l));
// //             Debug.LogError(dist);
// //                     if(minDist>dist&&dist>0.01f){
// //                         minPos = i;
// //                         minDist = dist;
// //                     }
// //                     i++;
// //                 }
// //         Debug.LogError(minDist);
// //         refractedRay.Point2 = refractedRay.pointOfIntersection(prismLineEquations[minPos]);
// //         refractedRay.Point2.z = refractedRay.Point1.z;
// //         drawLine.drawLine(refractedRay);
// //         // drawLine.drawLine(refractedRay1);
// //         float refractedAngle2 =90f-refractedRay.angleBetween(prismLineEquations[minPos]);
// //         float emergentAngle;
// //         if(Math.Sin(degToRad(refractedAngle2))*getRefractiveIndex()<1){
// //         emergentAngle = radToDeg((float)Math.Asin((float)Math.Sin(degToRad(refractedAngle2))*getRefractiveIndex()));
// //         }
// //         else{
// //             Debug.LogError("TIRRR");
// //             emergentAngle = refractedAngle2;
// //         }
// //         Debug.LogError("RF:"+refractedAngle2);
// //         Debug.LogError("RF:"+Math.Sin(degToRad(refractedAngle2)));
// //         Debug.LogError("RF:"+degToRad(refractedAngle2)*getRefractiveIndex());
// //         Debug.LogError(emergentAngle);
// //         LineEquation refractedRay2 = rotatedLine(refractedAngle2-emergentAngle,refractedRay,refractedRay.pointOfIntersection(prismLineEquations[minPos]));
// //         LineEquation refractedRay3 = rotatedLine(-refractedAngle2+emergentAngle,refractedRay,refractedRay.pointOfIntersection(prismLineEquations[minPos]));
// //         // drawLine.drawLine(refractedRay2);
// //         if(refractedRay2.angleBetween(prismLineEquations[minPos])>refractedRay3.angleBetween(prismLineEquations[minPos])){
// //         drawLine.drawLine(refractedRay3);
// //         }
// //         else{
// //             drawLine.drawLine(refractedRay2);
// //         }
// //         float angleOfDeviation = incidentAngle + emergentAngle - getAngleOfPrism();
// //     }


// //     public float degToRad(float deg){
// //         return(deg*(float)Math.PI/180f);
// //     }
// //     public float radToDeg(float rad){
// //         return(rad*180f/(float)Math.PI);
// //     }

// //     LineEquation rotatedLine(float angle,LineEquation l,Vector3 point){
// //         LineEquation l2 = new LineEquation();
// //         float rad = degToRad(angle);
// //         float a = l.b*(float)Math.Sin(rad)+l.a*(float)Math.Cos(rad);
// //         float b = l.b*(float)Math.Cos(rad)-l.a*(float)Math.Sin(rad);
// //         float c = l.a*point.x+l.b*point.y+l.c-a-b;
// //         l2.a = a;
// //         l2.b = b;
// //         l2.c = c;
// //         l2.Point1 = point;
// //         l2.Point2 = new Vector3();
// //         l2.Point2.x = point.x-20;
// //         l2.Point2.y = ((-a*point.x*(-20))-c)/b;
// //         l2.Point2.z = 0.2f;
// //         l2.Point1.z = 0.2f;
// //         return l2;
// //     }




//         /// <summary>
//     /// Function to draw all lines on prism after incident line strikes on prism
//     /// </summary>
//     /// <param name="line1">Prism Surface 1</param>
//     /// <param name="line2">Incident Ray</param>
//     /// <param name="prismLineEquations">List of Prism Surfaces, assumed to be closed</param>
//     public void Calculation(LineEquation line1, LineEquation line2, List<LineEquation> prismLineEquations)
//     {
//         LineEquation GetRefractedRay(LineEquation incident, LineEquation Surface, double deviation, Vector3 point, bool AwayFromNormal)
//         {
//             var RefractedLineCandid1 = Rotate(deviation, incident, point);
//             var RefractedLineCandid2 = Rotate(-deviation, incident, point);

//             var angle1 = 90 - Surface.AngleOfIntersection(RefractedLineCandid1);
//             var angle2 = 90 - Surface.AngleOfIntersection(RefractedLineCandid2);


//             if ((angle1 < angle2 && !AwayFromNormal) || (angle1 > angle2 && AwayFromNormal))
//                 return RefractedLineCandid1;
//             else
//                 return RefractedLineCandid2;
//         }

//         (LineEquation Surface, Vector3 Point) GetInterSectionPoint(LineEquation[] CandidateLines, LineEquation NonCandidateLine, LineEquation StrikingLine)
//         {
//             bool BetweenPoints(LineEquation l1, LineEquation l2, LineEquation target, Vector3 p)
//             {
//                 Vector3 v1 = l1.Intersect(target);
//                 Vector3 v2 = l2.Intersect(target);

//                 var f1 = Math.Abs((p.x - v1.x) / (v2.x - v1.x));
//                 var f2 = Math.Abs((p.y - v1.y) / (v2.y - v1.y));

//                 return
//                         (0 <= f1) && (f1 <= 1) &&
//                         (0 <= f2) && (f2 <= 1);
//             }

//             var Line1 = CandidateLines[0];
//             var Line2 = CandidateLines[1];

//             if (BetweenPoints(Line2, NonCandidateLine, Line1, StrikingLine.Intersect(Line1)))
//                 return (Line1, StrikingLine.Intersect(Line1));
//             else
//                 return (Line2, StrikingLine.Intersect(Line2));
//         }

//         #region Initialization

//         var CriticalAngle = Math.Asin(1 / RefractiveIndex) * 180 / Math.PI;
//         Debug.Log(CriticalAngle);
//         LineEquation PrismSurface = line1;
//         LineEquation IncidentRay = line2;

//         #endregion

//         Vector3 Point1 = default, Point2 = default, Point3 = default, Point4 = default;
//         bool TIR2 = default, TIR3 = default;

//         #region Surface1 Incident

//         Point1 = PrismSurface.Intersect(IncidentRay);
//         var incidentAngle = 90 - PrismSurface.AngleOfIntersection(IncidentRay); // From Normal

//         #endregion

//         Debug.Log(string.Format("Surface 1, Incident Angle : {0} and Point : {1} and Slope : {2}", incidentAngle, ToStr(Point1), IncidentRay));

//         #region Surface1 Refraction
//         double RefractionAngle = Math.Abs((float)(Math.Asin(Math.Sin(incidentAngle * Math.PI / 180) / RefractiveIndex) * 180 / Math.PI)); // From Normal

//         LineEquation RefractedLine = GetRefractedRay(IncidentRay, PrismSurface, incidentAngle - RefractionAngle, Point1, AwayFromNormal: false);

//         #endregion

//         Debug.Log(string.Format("Surface 1, Refraction Angle : {0}, Refraction Line : {1}\n", RefractionAngle, RefractedLine));

//         #region Surface2 Incident

//         var output = GetInterSectionPoint(
//             CandidateLines: (from x in prismLineEquations where x != PrismSurface select x).ToArray(),
//             PrismSurface,
//             RefractedLine);

//         Point2 = output.Point;
//         PrismSurface = output.Surface;

//         IncidentRay = RefractedLine;
//         incidentAngle = 90 - PrismSurface.AngleOfIntersection(IncidentRay);

//         #endregion

//         Debug.Log(string.Format("Surface 2, Incident Angle : {0} and Point : {1} and Slope : {2}", incidentAngle, ToStr(Point1), IncidentRay));
//         #region Surface2 Refraction

//         if (incidentAngle < CriticalAngle)
//         {
//             TIR2 = false;
//             RefractionAngle = Math.Abs((float)Math.Asin(RefractiveIndex * Math.Sin(incidentAngle * Math.PI / 180))) * 180 / Math.PI;
//             RefractedLine = GetRefractedRay(IncidentRay, PrismSurface, incidentAngle - RefractionAngle, Point2, AwayFromNormal: true);
//         }
//         else
//         {
//             TIR2 = true;
//             RefractionAngle = incidentAngle;

//             // If angle i < 45*, we take that ray that is towards normal, otherwise away from normal
//             RefractedLine = GetRefractedRay(
//                 IncidentRay,
//                 PrismSurface,
//                 2 * RefractionAngle,
//                 Point2,
//                 AwayFromNormal: (RefractionAngle < 45) ? false : true);
//         }
//         #endregion

//         Debug.Log(string.Format("Surface 2, Refraction Angle : {0}, Refraction Line : {1}\n", RefractionAngle, RefractedLine));
//         if (!TIR2)
//             goto end;

//         #region Surface3 Incident

//         output = GetInterSectionPoint(
//             CandidateLines: (from x in prismLineEquations where x != PrismSurface select x).ToArray(),
//             NonCandidateLine: PrismSurface,
//             RefractedLine);

//         Point3 = output.Point;
//         PrismSurface = output.Surface;

//         IncidentRay = RefractedLine;
//         incidentAngle = 90 - PrismSurface.AngleOfIntersection(IncidentRay);

//         #endregion

//         Debug.Log(string.Format("Surface 3, Incident Angle : {0} and Point : {1} and Slope : {2}", incidentAngle, ToStr(Point1), IncidentRay));

//         #region Surface3 Refraction

//         if (incidentAngle < CriticalAngle)
//         {
//             TIR3 = false;

//             RefractionAngle = Math.Abs((float)Math.Asin(RefractiveIndex * Math.Sin(incidentAngle * Math.PI / 180))) * 180 / Math.PI;
//             RefractedLine = GetRefractedRay(IncidentRay, PrismSurface, incidentAngle - RefractionAngle, Point3, AwayFromNormal: true);
//         }
//         else
//         {
//             TIR3 = true;
//             RefractionAngle = incidentAngle;

//             RefractedLine = GetRefractedRay(
//                 IncidentRay,
//                 PrismSurface,
//                 2 * RefractionAngle,
//                 Point3,
//                 AwayFromNormal: (RefractionAngle < 45) ? false : true);
//         }

//         #endregion

//         Debug.Log(string.Format("Surface 3, Refraction Angle : {0}, Refraction Line : {1}", RefractionAngle, RefractedLine));
//         if (!TIR3)
//             goto end;

//         #region Surface4 Incident

//         output = GetInterSectionPoint(
//             CandidateLines: (from x in prismLineEquations where x != PrismSurface select x).ToArray(),
//             NonCandidateLine: PrismSurface,
//             IncidentRay);

//         Point4 = output.Point;
//         PrismSurface = output.Surface;

//         IncidentRay = RefractedLine;
//         incidentAngle = 90 - PrismSurface.AngleOfIntersection(IncidentRay);

//         #endregion

//         Debug.Log(string.Format("Surface 4, Incident Angle : {0} and Point : {1}", incidentAngle, ToStr(Point4)));

//     end:
//     result = new List<LineEquation>();
//         // When line passes from Prism
//         if (!TIR2)
//         {/*// Variables of use:
//             Point1;
//             Point2;
//             RefractedLine;*/
//             Debug.LogError("First");
//             LineEquation l1 = new LineEquation(Point1,Point2);
//             result.Add(l1);
//             result.Add(RefractedLine);
//         }
//         else if (!TIR3)
//         {/*// Variables of use:
//             Point1;
//             Point2;
//             Point3;
//             RefractedLine;*/
//             Debug.LogError("Second");
//             LineEquation l1 = new LineEquation(Point1,Point2);
//             result.Add(l1);
//             LineEquation l2 = new LineEquation(Point2,Point3);
//             result.Add(l2);
//             result.Add(RefractedLine);
//         }
//         else
//         {/* Point1;
//             Point2;
//             Point3;
//             Point4;
//             RefractedLine;*/
//             Debug.LogError("Third");
//             LineEquation l1 = new LineEquation(Point1,Point2);
//             result.Add(l1);
//             LineEquation l2 = new LineEquation(Point2,Point3);
//             result.Add(l2);
//             LineEquation l3 = new LineEquation(Point3,Point4);
//             result.Add(l3);
//             result.Add(RefractedLine);
//         }
//     }


//     public float DegToRadian(float deg)
//     {
//         return (deg * (float)Math.PI / 180f);
//     }

//     LineEquation Rotate(double angle, LineEquation l, Vector3 point)
//     {
//         angle *= Math.PI / 180;
//         var NewSlope = (l.Slope + Math.Tan(angle)) / (1 - l.Slope * Math.Tan(angle));

//         return new LineEquation(point, Math.Atan(NewSlope) * 180 / Math.PI);
//     }

//     public string ToStr(Vector3 v)
//     {
//         return string.Format("({0},{1},{2})", v.x, v.y, v.z);
//     }


// }
