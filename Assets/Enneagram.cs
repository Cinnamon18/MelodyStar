using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UnityEngine.UI.Extensions.UIPolygon))]
public class Enneagram : MonoBehaviour
{
    public int numTraits;
    public float maxRadius;
    float[] traitValues;
    Vector3[] vertices;
    private Mesh mesh;

    // Start is called before the first frame update
    void Awake()
    {
        CreateEnneagram();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateEnneagram() {

        if (true || traitValues == null) {
            traitValues = new float[5];

            for (int i = 0; i < 5; i++) {
                traitValues[i] = Random.Range(.3f, .7f);
                //traitValues[i] = 1;
            }
        }
        UnityEngine.UI.Extensions.UIPolygon poly = GetComponent<UnityEngine.UI.Extensions.UIPolygon>();
        for (int i = 0; i < traitValues.Length; i++) {
            poly.VerticesDistances[i] = traitValues[i];
        }

        if (false) {
            vertices = new Vector3[numTraits + 1];

            vertices[0] = Vector3.zero;

            float degreesPerTrait = 360.0f / numTraits;
            for (int i = 0; i < numTraits; i++) {
                vertices[i + 1] = Quaternion.AngleAxis(degreesPerTrait * i, Vector3.forward) * Vector3.up * maxRadius * traitValues[i];

            }

            int[] triangles = new int[3 * (numTraits+1)];
            for (int i = 0; i < numTraits; i++) {

                triangles[i*3] = 0;
                triangles[(i*3)+2] = (i+1) >= vertices.Length ? 1 : i + 1;
                triangles[i*3 + 1] = (i+2) >= vertices.Length ? 1 : i + 2;
            }
            //triangles[(numTraits)*3] = 0;
            //triangles[((numTraits)*3)+2] = numTraits;
            //triangles[(numTraits)*3 + 1] = 1;

            mesh = new Mesh();
            mesh.name = "Enneagram Mesh";
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}
