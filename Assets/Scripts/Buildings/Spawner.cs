using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum BuilderStates : ushort {Idle, Building}
    public BuilderStates currentBuilderState;
    
    private GameObject cube;

    [SerializeField] 
    private Camera cam;
    private CameraBehaivior camScript;


    // Start is called before the first frame update
    void Start()
    {
        currentBuilderState = BuilderStates.Idle;
        camScript = cam.GetComponent<CameraBehaivior>();
    }
    void Update()
    {
        //Debug.Log(currentBuilderState);

        if(currentBuilderState == BuilderStates.Building)
        {
            building();
        }
    }

    public void createBuilding(string name)
    {
        GameObject buildings = Resources.Load(name) as GameObject;
        cube = Instantiate(buildings) as GameObject;
        currentBuilderState = BuilderStates.Building;
    }

    void building()
    {
        //cube.transform.position = new Vector3 (Mathf.RoundToInt(camScript.getmouseTerrainPos()[0]),0,Mathf.RoundToInt(camScript.getmouseTerrainPos()[2]));
        cube.transform.position = camScript.getMouseTerrainRaycastHit().transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            currentBuilderState = BuilderStates.Idle;
        }
    }
}
