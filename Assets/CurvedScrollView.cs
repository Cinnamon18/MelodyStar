using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Evaluate(float distanceAlongCurve) {

    }

    Vector3 EvaluateBetweenTwoNodes(ScrollSplineNode a, ScrollSplineNode b, float t) {
        return Utility.TSpline(a.transform.position, a.transform.rotation.eulerAngles,
        b.transform.position, b.transform.rotation.eulerAngles,
        t
        );
    }
}
