using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Animator ani;
    public Material newElementMaterial;
    public GameObject humanoid_mesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {
            
          
            StartCoroutine(TimerToSwtich());
            ani.SetTrigger("Open");
            Destroy(this.gameObject, 0.5f);
        }
    }

    IEnumerator TimerToSwtich()
    {
        yield return new WaitForSeconds(0.4f);
        humanoid_mesh.GetComponent<SkinnedMeshRenderer>().material = newElementMaterial;

    }
}
