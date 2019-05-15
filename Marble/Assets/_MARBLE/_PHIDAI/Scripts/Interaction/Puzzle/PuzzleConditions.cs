using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConditions : MonoBehaviour
{
    public int amountOfCompletedPieces;
    public int requiredAmountToComplete;
    public GameObject objEvent;
    public bool complete = false;
    bool finishedAnimation = false;
    Animator ani;
    SkinnedMeshRenderer smr_objEvent;
    float t = 0;

    private void Start()
    {
       smr_objEvent = objEvent.GetComponent<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        if (amountOfCompletedPieces >= requiredAmountToComplete) { complete = true; Debug.Log("Puzzle " + gameObject.name + " is done!"); }

        if (complete && !finishedAnimation) DoCompletedAction();
    }

    void DoCompletedAction()
    {
       smr_objEvent.SetBlendShapeWeight(0, Mathf.Lerp(100, 0, t));
       t += 1.5f * Time.deltaTime;

        if (smr_objEvent.GetBlendShapeWeight(0) == 0) { finishedAnimation = true;  objEvent.GetComponent<MeshCollider>().convex = false; }
    }

}
