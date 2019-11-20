using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spliny : MonoBehaviour
{

    public Transform nodeHolder;

    public float totalSplineLength;

    public float cornerStrengthScale = 100;

    public Color debugColor;
    public bool updateEveryFrame = true;

    protected ScrollSplineNode[] splineNodes;

    // Start is called before the first frame update
    void Start()
    {
        CalculatePercentagesAlongSpline();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Evaluate(float distanceAlongCurve) {

        float percentCovered = 0;

        if (splineNodes == null) return Vector3.zero;

        if (distanceAlongCurve == 0) {
            return splineNodes[0].transform.position;
        }
        if (distanceAlongCurve == 1) {
            return splineNodes[splineNodes.Length-1].transform.position;
        }
        if (distanceAlongCurve == .5f) {
            distanceAlongCurve = .5f;
        }

        for (int i = 0; i < splineNodes.Length - 1; i++) {

            percentCovered = splineNodes[i].percentageAlongTotalSpline;
            if (splineNodes[i+1].percentageAlongTotalSpline > distanceAlongCurve) {
                // this is the segment of nodes along the curve that distanceAlongCurve resides in!!
                float distanceAlongCurveExceptForSegment = (distanceAlongCurve - percentCovered) 
                / (splineNodes[i+1].percentageAlongTotalSpline - percentCovered);

                return EvaluateBetweenTwoNodes(splineNodes[i], splineNodes[i+1], distanceAlongCurveExceptForSegment);
            }

        }

        Debug.Log("ERROR UwU");
        return Vector3.zero;
    }

    void GetSplineNodes() {
        if (nodeHolder != null) {
            splineNodes = nodeHolder.GetComponentsInChildren<ScrollSplineNode>();
        }
    }

    Vector3 EvaluateBetweenTwoNodes(ScrollSplineNode a, ScrollSplineNode b, float t) {

        Vector3 aPos = a.transform.position;
        Vector3 dirA = a.transform.right * a.transform.localScale.x * cornerStrengthScale;
        Vector3 bPos = b.transform.position;
        Vector3 dirB = -b.transform.right * b.transform.localScale.x * cornerStrengthScale;
        return Utility.TSpline(aPos, dirA, bPos, dirB, t);
    }

    float GetTotalBezierSplineLength() {

        return 0;
    }

    public void CalculatePercentagesAlongSpline() {

        if (splineNodes == null) {
        }
        GetSplineNodes();


        if (splineNodes != null && splineNodes.Length > 0) {
            float[] lengths = new float[splineNodes.Length];

            splineNodes[0].arcLengthFromLastToThis = 0;
            splineNodes[0].percentageAlongTotalSpline = 0;

            for (int i = 1; i < splineNodes.Length; i++) {

                Vector3 a = splineNodes[i-1].transform.position;
                Vector3 dirA = splineNodes[i-1].transform.right * splineNodes[i-1].transform.localScale.x * cornerStrengthScale;
                Vector3 b = splineNodes[i].transform.position;
                Vector3 dirB = -splineNodes[i].transform.right * splineNodes[i].transform.localScale.x * cornerStrengthScale;

                lengths[i] = Utility.ApproxTSplineArcLength(a, dirA, b, dirB, 5);

                splineNodes[i].arcLengthFromLastToThis = lengths[i];               
            }

            totalSplineLength = 0;
            for (int i = 1; i < splineNodes.Length; i++) {

                totalSplineLength += splineNodes[i].arcLengthFromLastToThis;                
            }

            float lengthCountedSoFar = 0;
            for (int i = 1; i < splineNodes.Length; i++) {
                lengthCountedSoFar += splineNodes[i].arcLengthFromLastToThis;
                splineNodes[i].percentageAlongTotalSpline = lengthCountedSoFar / totalSplineLength;                
            }
        }
    }

    public int debugSubdivisions = 10;
    Vector3[] points;

    void OnDrawGizmos() {
        if (updateEveryFrame) {
            CalculatePercentagesAlongSpline();
        }
        if (splineNodes ==  null) {
            CalculatePercentagesAlongSpline();
        }
        
        points = new Vector3[debugSubdivisions];

        float step = 1.0f / debugSubdivisions;

        for (int i = 0; i < debugSubdivisions; i++) {
            points[i] = Evaluate(step * i);
        }
        Utility.DrawLinesBetweenPoints(points, debugColor);
    }
}
