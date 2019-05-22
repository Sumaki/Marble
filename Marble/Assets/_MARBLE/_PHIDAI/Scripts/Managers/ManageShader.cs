using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShader : MonoBehaviour
{
    [SerializeField]
    private Vector3 grassPosition;
    
    MeshRenderer meshRenderer = null;

    private void Awake()
    {
       // grassPosition = gameObject.GetComponent<MeshRenderer>().material.GetVector("_worldPosition");
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        grassPosition = gameObject.GetComponent<MeshRenderer>().transform.position;

        meshRenderer.material.SetVector("_worldPosition", grassPosition);
        Debug.Log("Mesh Renderer Material Position: " + meshRenderer.material.GetVector("_worldPosition"));
    }

}
