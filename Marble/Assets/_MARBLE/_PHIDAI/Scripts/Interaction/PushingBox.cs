using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingBox : MonoBehaviour
{

    public GameObject player;
    public bool pushing = false;
    CharacterController cc;
    CharacterAnimationState characterState; 

    Vector3 p1;


    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        characterState = player.GetComponent<CharacterAnimationState>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Final position of spherecast: " + p1);

        if(characterState.state == CharacterAnimationState.CharacterState.push)
            Check();
    }

    void Check()
    {
        RaycastHit hit;
       // Vector3 characterDirection = cc.transform.forward;
        p1 = cc.transform.position + cc.transform.forward * 4f;

      // Note: Maybe raycast within the obj to have a easier check to push?
    
        if(Physics.SphereCast(p1 + Vector3.up, 1.5f, cc.transform.forward, out hit, 10f))
        {
            
            Debug.Log("I have spherecasted into: " + hit.transform.name);
            Debug.Log("SphereCast Hit position: " + hit.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(p1 + Vector3.up, 1.5f);
        DebugExtension.DrawCapsule(p1 + Vector3.up, p1 + Vector3.up * 2);
    }


}
