using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Face{
    public List<Vector3> vertices {get; private set;}
    public List<int> triangle {get; private set;}
    public List<Vector2> uvs {get; private set;}
    public Face(List<Vector3> vertices,List<int> triangle,List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangle = triangle;
        this.uvs = uvs;
    }
}

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Hexagon : MonoBehaviour
{
    // Structure to help generate a Hexagon. 6 Faces are building one Hexagon.
    private List<Face> faces = new List<Face>();
    public List<Vector3> vertices {get; private set;} = new List<Vector3>();
    public List<int> triangle {get; private set;} = new List<int>();
    public List<Vector2> uvs {get; private set;} = new List<Vector2>();

    // Setting for the size of an Hexagon
    public float innerRadius = 0.8f;
    public float outerRadius = 1f;
    public float height = 0;    

    public Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;


    public void DrawFace()
    {
        for(int i = 0; i<6;i++)
        {
            faces.Add(createFaces(i));
        }
    }

    // draws one of six faces for a Hexagon. Parameter i is a counter to see if Hexagon is complete.
    public Face createFaces(int i)
    {
            Vector3 Point1 = getPointRotation(i,innerRadius);
            Vector3 Point2 = getPointRotation((i <5) ? i + 1 : 0, innerRadius);
            Vector3 Point3 = getPointRotation((i <5) ? i + 1 : 0, outerRadius);
            Vector3 Point4 = getPointRotation(i,outerRadius);

            List<Vector3> ver = new List<Vector3>() { Point1, Point2, Point3, Point4 };
            List<int> tri = new List<int>() {0,1,2,2,3,0};
            List<Vector2> uv = new List<Vector2>();

            return new Face(ver,tri,uv);

    }

    // Rotates a given point for index * 60 degrees.
    private Vector3 getPointRotation(int index, float radius)
    {
        float angleDeg = index * 60;
        float angleRad = Mathf.PI / 180f * (angleDeg);
        return new Vector3(radius * Mathf.Cos(angleRad), radius * height, radius * Mathf.Sin(angleRad));
    }

    // Draws a Hexagon Mesh by drawing faces and combine them to a Hexagon.
    public void DrawHexagon()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        DrawFace();

        for(int i = 0; i< faces.Count; i++)
        {
            vertices.AddRange(faces[i].vertices);
            int offset = 4 * i;
            foreach(int tri in faces[i].triangle)
            {
                triangle.Add(tri + offset);
            }
        }
        
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangle.ToArray();
        mesh.RecalculateNormals();
        
        meshFilter.mesh = mesh;
    }
}
