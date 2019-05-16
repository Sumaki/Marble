using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessShaderGraphProperties : MonoBehaviour
{
    [SerializeField] private float materialRadius = 1f;

    private MeshRenderer meshRenderer = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // the string used here is the one defined in the shader graph
        meshRenderer.material.SetFloat("_Radius", materialRadius);
    }
}
