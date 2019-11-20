using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

using SplineInfo = System.Collections.Generic.List<PosDirPair>;
public static class Utility
{

    public static void DrawArrowsToChildren(Transform parentBone) {

        foreach (Transform childBone in parentBone) {

            Vector3 arrowDir = childBone.position - parentBone.position;
            DrawGizmoArrow(parentBone.position, childBone.position, Color.white);

            DrawArrowsToChildren(childBone);
        }
    }

    public static void DrawLinesToChildren(Transform parentBone) {

        Gizmos.color = Color.white;
        foreach (Transform childBone in parentBone) {

            Vector3 arrowDir = childBone.position - parentBone.position;
            Gizmos.DrawLine(parentBone.position, childBone.position);

            DrawLinesToChildren(childBone);
        }
    }

    public static void DrawLinesBetweenPoints(Vector3[] points, Color color) {

        Gizmos.color = color;
        for (int i = 0; i < points.Length - 1; i++) {

            Gizmos.DrawLine(points[i], points[i+1]);
        }
    }
    

    public static void DrawGizmoArrow(Vector3 start, Vector3 end, Color color) {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
        Vector3 dir = end - start;

        float arrowHeadLength = .25f;
        start = end - dir.normalized * arrowHeadLength + Vector3.up * arrowHeadLength;
        Gizmos.DrawLine(start, end);

        start = end - dir.normalized * arrowHeadLength - Vector3.up * arrowHeadLength;
        Gizmos.DrawLine(start, end);
    }

    public static int GetDeepChildCount(Transform parent) {
        int childCount = parent.childCount;
        foreach (Transform child in parent) {
            childCount += GetDeepChildCount(child);
        }
        return childCount;
    }
    public static Vector3 TSpline(Vector3 a, Vector3 dirA, Vector3 b, Vector3 dirB, float t) {

        Vector3 ad = a + dirA;
        Vector3 bd = b + dirB;

        return Bezier(a, ad, bd, b, t);
    }
    public static Vector3 TSpline(List<PosDirPair> pointsInfo, float t) {

        int whereToStart = (int) ((pointsInfo.Count-1) * t);

        

        if (t >= 1) {
            return pointsInfo[pointsInfo.Count - 1].pos;
        } else if (t <= 0) {
            return pointsInfo[0].pos;
        }

        Vector3 a       = pointsInfo[whereToStart].pos;
        Vector3 b       = pointsInfo[whereToStart+1].pos;
        Vector3 dirA    = b - a;
        Vector3 dirB;
        if (whereToStart+1 == pointsInfo.Count - 1) {

            dirB = pointsInfo[whereToStart+1].dir.normalized;
        } else {
            dirB = pointsInfo[whereToStart+2].pos - b;
        }

        dirA    = pointsInfo[whereToStart].dir * 1;
        dirB    = pointsInfo[whereToStart+1].dir * 1;

        Vector3 ad = a + dirA;
        Vector3 bd = b - dirB;
        float subT = (t * (pointsInfo.Count-1)); //(t*(pointsInfo.Count-1));// - (int)(t*pointsInfo.Count-1);
        subT -= (float) System.Math.Truncate(subT);
        //Debug.Log("Start: " + whereToStart + ", subT: " + subT);
        return Bezier(a, ad, bd, b, subT);
    }
    public static Vector3 Bezier(List<Vector3> pointsInfo, float t) {

        int whereToStart = (int) ((pointsInfo.Count-1) * t);

        if (t >= 1) {
            return pointsInfo[pointsInfo.Count - 1];
        } else if (t <= 0) {
            return pointsInfo[0];
        }

        if (pointsInfo.Count == 3) {

            Vector3 a       = pointsInfo[0];
            Vector3 b       = pointsInfo[1];
            Vector3 c       = pointsInfo[2];
            return Bezier(a, b, c, t);
        }



        return Vector3.zero;
    }
    public static Vector3 Bezier(Vector3 A, Vector3 B, float t) 
    {
        return Vector3.Lerp(A, B, t);  
    }
    
    public static Vector3 Bezier(Vector3 A, Vector3 B, Vector3 C, float t) 
    {
        Vector3 S = Bezier(A,B,t);
        Vector3 E = Bezier(B,C,t);
        return  Bezier(S,E,t);
    }
    
    public static Vector3 Bezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t) 
    {
        Vector3 S = Bezier(A,B,C,t);
        Vector3 E = Bezier(B,C,D,t);
        return  Bezier(S,E,t);
    }
    

    /*public static float GetApproxBezierArcLength(Vector3 actualA, Vector3 actualB, Vector3 actualC,
     Vector3 tempA, Vector3 tempB, Vector3 tempC, float fractionOfTotalCurveCoveredByTemp, float fractionFromStartOfCurve, int recursions) {

        if (recursions == 0) {
            float chord = (tempC-tempA).magnitude;
            float cont_net = (tempA - tempB).magnitude + (tempC - tempB).magnitude;

            float app_arc_length = (cont_net + chord) / 2;
        }

        Vector3 quarter = Bezier(actualA, actualB, actualC, fractionOfTotalCurveCoveredByTemp * .25f);
        Vector3 halfway = Bezier(actualA, actualB, actualC, fractionOfTotalCurveCoveredByTemp * .5f);
        Vector3 threeQuarters = Bezier(actualA, actualB, actualC, fractionOfTotalCurveCoveredByTemp * .75f);

        return GetApproxBezierArcLength(actualA, actualB, actualC, quarter, tempA, b, fractionOfTotalCurveCoveredByTemp / 2, recursions - 1);
    }*/

    public static float ApproxBezierArcLength(Vector3 a, Vector3 b, Vector3 c, float start, float end, int recursions) {

        if (recursions == 0) {
            Vector3 point1 = Bezier(a, b, c, start);
            Vector3 point2 = Bezier(a, b, c, (end - start) / 2);
            Vector3 point3 = Bezier(a, b, c, end);
            float chordLength1 = (point2 - point1).magnitude;
            float chordLength2 = (point3 - point2).magnitude;

            return chordLength1 + chordLength2;
        } else  {
            float midpoint = (end - start) / 2.0f;
            float recursiveFirstHalfLength = ApproxBezierArcLength(a, b, c, start, midpoint, recursions - 1);
            float recursiveSecondHalfLength = ApproxBezierArcLength(a, b, c, midpoint, end, recursions - 1);

            return recursiveFirstHalfLength + recursiveSecondHalfLength;
        }
    }

    public static float ApproxTSplineArcLength(Vector3 a, Vector3 dirA, Vector3 b, Vector3 dirB, int recursions) {
        
        Vector3 midpoint = TSpline(a, dirA, b, dirB, .5f);
        return ApproxBezierArcLength(a, midpoint, b, 0, 1, recursions);
    }


    // return true if a line starting at point and going in direction passes through a triangle abc
    // https://stackoverflow.com/questions/53962225/how-to-know-if-a-line-segment-intersects-a-triangle-in-3d-space
    public static bool RaycastToTriangle(Vector3 start, Vector3 dir, Vector3 a, Vector3 b, Vector3 c) {
        float tetraVol1 = TetraVol(a, b, start, start + dir);
        float tetraVol2 = TetraVol(b, c, start, start + dir);
        float tetraVol3 = TetraVol(c, a, start, start + dir);
        if (tetraVol1 == 0 || tetraVol2 == 0 || tetraVol3 == 0) return false; // start or end lies on the triangle
        if ((tetraVol1 < 0 == tetraVol2 < 0) && (tetraVol1 < 0 == tetraVol3 < 0)) return true; // signs are same so no intersection
        return false; // signs are different so the line intersects
    }

    public static bool RaycastToPlane(Vector3 start, Vector3 end, Vector3 a, Vector3 b, Vector3 c) {
        float tetraVol1 = TetraVol(a, b, c, start);
        float tetraVol2 = TetraVol(a, b, c, end);
        if (tetraVol1 == 0 || tetraVol2 == 0) return true; // start or end lies on the triangle
        if (tetraVol1 < 0 == tetraVol2 < 0) return false; // signs are same so no intersection
        return true; // signs are different so the line intersects
    }

    // returns the volume of a tetrahedron given four points
    public static float TetraVol(Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
        return (Vector3.Dot(a-d, Vector3.Cross(b-d, c-d))) / 6;
    }

    // Adapted from https://github.com/sergarrido/random/tree/master/circle3d
    public static Circle GetCircleThru3Points(Vector3 a, Vector3 b, Vector3 c) {

        if (false) {
        Vector3 edge1 = b-a;
        Vector3 edge2 = c-a;
        Vector3 edge3 = c-b;

        Vector3 normal = Vector3.Cross(edge1, edge2);

        float wsl = normal.sqrMagnitude;

        float iwsl2 = 1f/(2f*wsl);
        float tt = Vector3.Dot(edge1, edge1);
        float uu = Vector3.Dot(edge2, edge2);
        float vv = Vector3.Dot(edge3, edge3);

        Vector3 center = a 
            + (edge2 * tt * (Vector3.Dot(edge2, edge3))
            - (edge1 * uu * (vv) * iwsl2 * .5f)
        );
        float rad = Mathf.Sqrt(tt * uu * vv * iwsl2 * .5f);
        Vector3 axis = normal / Mathf.Sqrt(wsl);

        return new Circle(center, axis, rad);
        } else if (false) {

            var v1 = b-a;
            var v2 = c-a;

            var aa = Vector3.Dot(a, a);
            var bb = Vector3.Dot(b, b);
            var ab = Vector3.Dot(a, b);

            var coeff = 1f / (2f * (aa * bb) - ab*ab);
            //var k1 = coeff * bb * (aa - ab);
            //var k2 = coeff * aa * (bb - ab);
            var k1 = .5f * (bb) * (aa - ab) / (aa*bb - ab*ab);
            var k2 = .5f * (aa) * (bb - ab) / (aa*bb - ab*ab);

            Vector3 center = a + k1*v1 + k2*v2;
            Vector3 axis = PlaneNormalOf3Points(a, b, c);
            float radius = (a - center).magnitude;

            return new Circle(center, axis, radius);
        } else {
            Vector3 v1 = b-a;
            Vector3 v2 = c-a;
            float v1v1, v2v2, v1v2;
            v1v1 = Vector3.Dot(v1, v1);
            v2v2 = Vector3.Dot(v2, v2);
            v1v2 = Vector3.Dot(v1, v2);

            float coeff = 0.5f/(v1v1*v2v2-v1v2*v1v2);
            float k1 = coeff*v2v2*(v1v1-v1v2);
            float k2 = coeff*v1v1*(v2v2-v1v2);
            c = a + v1*k1 + v2*k2; // center

            float radius = Mathf.Sqrt(Vector3.Dot(c-a, c-a));
            Vector3 axis = PlaneNormalOf3Points(a, b, c);
            
            return new Circle(c, axis, radius);
        }
    }

    public static Vector3 PlaneNormalOf3Points(Vector3 a, Vector3 b, Vector3 c) {
        return Vector3.Cross(a - b, c - b);
    }

    public static Vector3[] GetCirclePoints(Circle c, Vector3 right, int numPoints = 8) {
        
        float degreeStep = 360f / numPoints;
        Vector3 offset = c.axis.normalized * c.radius;

        Vector3[] verts = new Vector3[numPoints];


        right = FindArbitraryOrthoVec(c.axis);

        Vector3 up = Vector3.Cross(c.axis,right);

        for (int i = 0; i < numPoints; i++) {
            Vector3 vec = c.center + Quaternion.AngleAxis(i * degreeStep, c.axis.normalized)*right * c.radius;
            verts[i] = vec;
        }
        return verts;
    }


    public static Vector3[] GetLoopPoints(Vector3[] points, int subdivisions, float cornerStrength, Vector3 scale) {
        Vector3[] loopPoints = new Vector3[subdivisions + 1];
        float step = 1f/loopPoints.Length;
        for (int i = 0; i < subdivisions; i++) {
            loopPoints[i] = Utility.SmoothLoopThruPoints(
                points, 
                i*step, 
                cornerStrength, 
                false, 
                scale
                );
        }
        loopPoints[subdivisions] = points[0];
        return loopPoints;
    }
    public static void SortSplinesClockwiseAroundAxis(ref List<SplineInfo> points, Vector3 center, Vector3 axis) {
        if (points.Count == 0) return;

        Vector3 arbitraryRight = points[0][1].pos - center;
        points = points.OrderBy(p => Vector3.SignedAngle(arbitraryRight, p[1].pos - center, axis)).ToList();
    }
    public static List<Vector3> SortPointsClockwiseAroundAxis(ref List<Vector3> points, Vector3 center, Vector3 axis, Vector3 right) {
        if (points.Count == 0) return null;


        Vector3 arbitraryRight = points[0] - center;
        var pointsOrdered = points.OrderBy(p => Vector3.SignedAngle(right, (p - center), axis));
        return pointsOrdered.ToList();
    }

    public static bool AClockwiseFromB(Vector3 a, Vector3 b, Vector3 center, Vector3 axis) {
        return axis.Dot((a-center).Cross(b-center)) < 0;
    }

    public static Vector3 FindArbitraryOrthoVec(Vector3 v) {
        float a = 1,b = 1;
        float c = (v.x*a + v.y*b)/-v.z; //+ v.z*c;
        return new Vector3(a, b, c).normalized;
    }
    public static Vector3[] MakeBezierCurve(Vector3[] arrayToCurve,float smoothness){
         List<Vector3> points;
         List<Vector3> curvedPoints;
         int pointsLength = 0;
         int curvedLength = 0;
         
         if(smoothness < 1.0f) smoothness = 1.0f;
         
         pointsLength = arrayToCurve.Length;
         
         curvedLength = (pointsLength*Mathf.RoundToInt(smoothness))-1;
         curvedPoints = new List<Vector3>(curvedLength);
         
         float t = 0.0f;
         for(int pointInTimeOnCurve = 0;pointInTimeOnCurve < curvedLength+1;pointInTimeOnCurve++){
             t = Mathf.InverseLerp(0,curvedLength,pointInTimeOnCurve);
             
             points = new List<Vector3>(arrayToCurve);
             
             for(int j = pointsLength-1; j > 0; j--){
                 for (int i = 0; i < j; i++){
                     points[i] = (1-t)*points[i] + t*points[i+1];
                 }
             }
             
             curvedPoints.Add(points[0]);
         }
         
         return(curvedPoints.ToArray());
     }
     
    public static Vector3 Neville(float a, Vector3 A, float b, Vector3 B, float t) 
    {
        // A point created by scaling the vector between A and B by (t-a)/(b-a) then adding to A
        return A + (t - a)/(b - a) * (B-A);
    }
  
    public static Vector3 Neville(float a, Vector3 A, float b, Vector3 B, float c, Vector3 C, float t) 
    {
        Vector3 S = Neville(a, A, b, B, t);
        Vector3 E = Neville(b, B, c, C, t);
        return Neville(a, S, c, E, t);
    }
  
    public static Vector3 Neville(float a, Vector3 A, float b, Vector3 B, float c, Vector3 C, float d, Vector3 D, float t) 
    {
        Vector3 S = Neville(a, A, b, B, c, C, t);
        Vector3 E = Neville(b, B, c, C, d, D, t);
        return Neville(a, S, d, E, t);
    }

    public static Vector3 PointAlongNevilleExpensive(Vector3[] points, float t) {

        switch (points.Length) {
            case (0):
            return Vector3.zero;
            case (1):
            return points[0];
            case (2):
            return Neville(0, points[0], 1f, points[1], t);
            case (3):
            return Neville(0, points[0], .5f, points[1], 1f, points[2], t);
            case (4):
            return Neville(0, points[0], .33f, points[1], .66f, points[2], 1f, points[3], t);
            default:
            Vector3[] firstGroup = points.SubArray(0, points.Length - 1);
            Vector3[] secGroup   = points.SubArray(1, points.Length - 1);
            
            
            Vector3 s = PointAlongNevilleExpensive(firstGroup, t);
            Vector3 e = PointAlongNevilleExpensive(secGroup  , t);
            return Neville(0, s, 1, e, t);
        }

    }

    public static float DistPointToLine(Vector3 p, Vector3 p1, Vector3 p2) {
        return ((p - p1) - (((p - p1).Dot(p2-p1)) / ((p2-p1).sqrMagnitude))*(p2-p1)).magnitude;
    }

    // TODO make work with curve not loop
    // TODO make scaling work nicer
    public static Vector3 SmoothLoopThruPoints(
        Vector3[] points, 
        float t, 
        float cornerStrength, 
        bool guessCornerStrength, 
        Vector3 scale
        ) {
        if (t == 0) return points[0];
        if (points.Length == 1) {
            return points[0];
        }
        if (points.Length == 2) {
            return points[Mathf.RoundToInt(t)];
        }
        List<PosDirPair> pointsInfo = new List<PosDirPair>();
        Vector3 a,b,c,d,e;
        int closestPointToTIndex = (int) (t * points.Length);

        if (points.Length == 3) {
            a = points[0];
            b = points[1];
            c = points[2];


            if (!guessCornerStrength) {

                pointsInfo.Add(new PosDirPair(a, (b-c).normalized * cornerStrength));
                pointsInfo.Add(new PosDirPair(b, (c-a).normalized * cornerStrength));
                pointsInfo.Add(new PosDirPair(c, (a-b).normalized * cornerStrength));
                pointsInfo.Add(new PosDirPair(a, (b-c).normalized * cornerStrength));
            } else {
                float triHeightA,triHeightB,triHeightC,dirScaleA,dirScaleB,dirScaleC; 
                triHeightA = DistPointToLine(a, b, c);
                triHeightB = DistPointToLine(b, a, a);
                triHeightC = DistPointToLine(c, a, b);

                dirScaleA = triHeightA;
                dirScaleB = triHeightB;
                dirScaleC = triHeightC;

                //dirScaleB = triHeightB / (c-a).magnitude;
                //dirScaleC = triHeightC / (d-b).magnitude;
                //dirScaleD = triHeightD / (e-c).magnitude;
                dirScaleA = triHeightA * (b-c).magnitude;
                dirScaleB = triHeightB * (c-a).magnitude;
                dirScaleC = triHeightC * (b-a).magnitude;

                dirScaleA *= cornerStrength;
                dirScaleB *= cornerStrength;
                dirScaleC *= cornerStrength;

                /*dirScaleA = 1;
                dirScaleB = 1;
                dirScaleC = 1;*/
                dirScaleA = cornerStrength;
                dirScaleB = cornerStrength;
                dirScaleC = cornerStrength;

                float scaleMag = scale.magnitude;

                dirScaleA *= scaleMag;
                dirScaleB *= scaleMag;
                dirScaleC *= scaleMag;

                pointsInfo.Add(new PosDirPair(a, (b-c) * dirScaleA));
                pointsInfo.Add(new PosDirPair(b, (c-a) * dirScaleB));
                pointsInfo.Add(new PosDirPair(c, (a-b) * dirScaleC));
                pointsInfo.Add(new PosDirPair(a, (b-c) * dirScaleA));
            }

            return TSpline(pointsInfo, t);
        } else {
        

        // only make pairs for the points close to t
        int prev = closestPointToTIndex - 1 < 0 ? points.Length - 1 : closestPointToTIndex - 1;
        int prevPrev = prev - 1 < 0 ? points.Length - 1 : prev - 1;
        int next = (closestPointToTIndex + 1) % points.Length;
        int nextNext = (next + 1) % points.Length;

        a = points[prevPrev];
        b = points[prev];
        c = points[closestPointToTIndex];
        d = points[next];
        e = points[nextNext];

        if (!guessCornerStrength) {
            pointsInfo.Add(new PosDirPair(b, (c-a).normalized * cornerStrength));
            pointsInfo.Add(new PosDirPair(c, (d-b).normalized * cornerStrength));
            pointsInfo.Add(new PosDirPair(d, (e-c).normalized * cornerStrength));
        } else {
            float triHeightB,triHeightC,triHeightD,dirScaleB,dirScaleC,dirScaleD; 
            triHeightB = DistPointToLine(b, a, c);
            triHeightC = DistPointToLine(c, b, d);
            triHeightD = DistPointToLine(d, c, e);

            dirScaleB = triHeightB;
            dirScaleC = triHeightC;
            dirScaleD = triHeightD;

            //dirScaleB = triHeightB / (c-a).magnitude;
            //dirScaleC = triHeightC / (d-b).magnitude;
            //dirScaleD = triHeightD / (e-c).magnitude;
            dirScaleB = triHeightB * (c-a).magnitude;
            dirScaleC = triHeightC * (d-b).magnitude;
            dirScaleD = triHeightD * (e-c).magnitude;

            dirScaleB *= cornerStrength;
            dirScaleC *= cornerStrength;
            dirScaleD *= cornerStrength;

            pointsInfo.Add(new PosDirPair(b, (c-a).Div(scale) * dirScaleB));
            pointsInfo.Add(new PosDirPair(c, (d-b).Div(scale) * dirScaleC));
            pointsInfo.Add(new PosDirPair(d, (e-c).Div(scale) * dirScaleD));
        }

        float step = 1f/(points.Length + 0);
        float newT = t;
        
        float left = prev * step;
        float right = next * step;
        
        if (t < step) {
            // prev and left wrap around
            newT += step;
            left += step; // should now be zero
            right += step;
        } else if (left > 0) {
            right -= left;
            newT -= left;
            left = 0;

        }

        newT = newT / (Mathf.Abs(right));

        return TSpline(pointsInfo, newT);
        }
    }

    // https://stackoverflow.com/questions/943635/getting-a-sub-array-from-an-existing-array
    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
    public static float Dot(this Vector3 data, Vector3 other)
    {
        return Vector3.Dot(data, other);
    }
    public static Vector3 Cross(this Vector3 data, Vector3 other)
    {
        return Vector3.Cross(data, other);
    }
    public static Vector3 Mult(this Vector3 data, Vector3 other)
    {
        return new Vector3(data.x * other.x, data.y * other.y, data.z * other.z);
    }
    public static Vector3 Div(this Vector3 data, Vector3 other)
    {
        return new Vector3(data.x / other.x, data.y / other.y, data.z / other.z);
    }
    public static Vector3 ProjOntoPlane(this Vector3 data, Vector3 normal)
    {
        return data - data.ProjOntoVector(normal);
    }
    public static Vector3 ProjOntoVector(this Vector3 data, Vector3 other)
    {
        return data.Dot(other)/other.sqrMagnitude * other;
    }
    public static IComparer<Vector3> CompareClockwise(Vector3 axis, Vector3 center) {
        return (IComparer<Vector3>) new CompareClockwiseHelper(axis, center);
    }
    private class CompareClockwiseHelper : IComparer<Vector3> {

        Vector3 axis;
        Vector3 center;

        int IComparer<Vector3>.Compare(Vector3 x, Vector3 y)
        {
            return (int) Vector3.SignedAngle(
                (Vector3) x - center, 
                (Vector3) y - center, 
                axis);
        }

        public CompareClockwiseHelper(Vector3 _axis, Vector3 _center) {
            axis = _axis;
            center = _center;
        }

    }
}
public class Circle {
    public Vector3 center;
    public Vector3 axis;
    public float radius;
    public Circle(Vector3 _center, Vector3 _axis, float _rad) {
        center = _center;
        axis = _axis;
        radius = _rad;
    }
}
public class PosDirPair {
    public Vector3 pos;
    public Vector3 dir;
    public PosDirPair(Vector3 p, Vector3 d) {
        pos = p;
        dir = d;
    }
    public PosDirPair(PosDirPair pair) {
        pos = pair.pos;
        dir = pair.dir;
    }
}