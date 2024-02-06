using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaivior : MonoBehaviour
{
    private const int mapLayer = 1 << 3;
    private RaycastHit mouseTerrainRaycastHit;
    Camera cam ;
    //new Vector3 mousePos; = Input.mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraMovement();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mapLayer)) 
        {
            mouseTerrainRaycastHit = hit;
        }
    }

    public void cameraMovement()
    {
        if (Input.GetKey("w"))
        {
            transform.position += Vector3.forward * 1.0f;
        }
        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * 1.0f;
        }
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left * 1.0f;
        }
        if (Input.GetKey("s"))
        {
            transform.position += Vector3.back * 1.0f;
        }
    }
    
    public RaycastHit getMouseTerrainRaycastHit()
    {
        return this.mouseTerrainRaycastHit;
    }
    public Vector3 getmouseTerrainPos()
    {
        return this.mouseTerrainRaycastHit.point;
    }
}
