using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public float scale = 1.0f;

    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int innerRadius = 8;
    [SerializeField] private int outerRadius = 10;

    [SerializeField] private Material deepWaterMaterial;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material grasMaterial;
    [SerializeField] private Material mountainMaterial;
    [SerializeField] private Material forestMaterial;
    

    // Creates a Grid System (x,y) of Hexagons
    void Start()
    {
        int mapLayer = LayerMask.NameToLayer("Map");

        // Random seed for two Perlin Noise origins
        float randomOriginTerrain = Random.Range(0, 1000);
        float randomOriginForest = Random.Range(0, 1000);


        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                GameObject hex = new GameObject($"Hex {x},{y}", typeof(Hexagon));
                
        
                hex.transform.Rotate(0,90,0, Space.Self); 
                hex.transform.position = new Vector3( (y%2 == 0) ? x * 1.732f * outerRadius: x * 1.732f * outerRadius + 0.866f * outerRadius,0,  y * 1.5f * outerRadius);

                Hexagon hexagon = hex.GetComponent<Hexagon>();
                hexagon.innerRadius = innerRadius;
                hexagon.outerRadius = outerRadius;
                hexagon.DrawHexagon();

                MeshCollider meshCollider = hex.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = hexagon.mesh;
                hex.layer = mapLayer;

                // Assigns the land type to the hextile using Perlin Noise
                float xCoord = randomOriginTerrain + (float) x / scale;
                float yCoord = randomOriginTerrain + (float) y / scale;
                float noise = Mathf.PerlinNoise(xCoord, yCoord);

                switch (noise)
                {
                    case float n when n >= 0.77f:
                        hexagon.GetComponent<MeshRenderer>().material = mountainMaterial;
                        break;

                    case float n when n >= 0.47f:

                        // Second Perling Noise is for assigning group of tiles to forest 
                        xCoord = randomOriginForest + (float) x / 4;
                        yCoord = randomOriginForest + (float) y / 4;
                        noise = Mathf.PerlinNoise(xCoord, yCoord);

                        if (noise >= 0.6) {
                            hexagon.GetComponent<MeshRenderer>().material = forestMaterial;
                        }
                        else {
                            hexagon.GetComponent<MeshRenderer>().material = grasMaterial;
                        }
                        break;

                    case float n when n >= 0.3f:
                        hexagon.GetComponent<MeshRenderer>().material = waterMaterial;
                        break;

                    default:
                        hexagon.GetComponent<MeshRenderer>().material = deepWaterMaterial;
                        break;
                }
            }
        }
        
    }
}
