using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphManager : MonoBehaviour
{
    [Header("Morph Model")]
    public GameObject blendObject;
    #region Morph Private Varibles
    SkinnedMeshRenderer smr_;
    bool morph = false;
    bool doneMorph;
    enum MorphState { ball, humanoid }
    MorphState state = MorphState.ball;
    float t = 0.0f;
    #endregion

   
    void Start()
    {
        smr_ = blendObject.GetComponent<SkinnedMeshRenderer>();
        doneMorph = true;
    }

    
    void Update()
    {
        //Debug.Log("Morph Status: " + morph);
        GlobalInput();
        Morph();
    }

    void GlobalInput()
    {
        if (Input.GetKeyDown(KeyCode.E)) // temp input
        {
            if (doneMorph && !morph)
            {
                doneMorph = false;
                morph = true;
            }
        }
    }

    void Morph()
    {   
        if(smr_.GetBlendShapeWeight(0) == 100 && doneMorph)
        {
            morph = false;
            t = 0.0f;           
        }

        if(smr_.GetBlendShapeWeight(0) == 0 && doneMorph)
        {
            morph = false;
            t = 0.0f;          
        }

        if (state == MorphState.ball && !doneMorph && morph)
        {          
            smr_.SetBlendShapeWeight(0, Mathf.Lerp(0, 100, t));
            t += 1f * Time.deltaTime;
                if(smr_.GetBlendShapeWeight(0) == 100) { doneMorph = true; state = MorphState.humanoid; }
        }

        if (state == MorphState.humanoid && !doneMorph && morph)
        {
            smr_.SetBlendShapeWeight(0, Mathf.Lerp(100, 0, t));
            t += 1f * Time.deltaTime;
            if (smr_.GetBlendShapeWeight(0) == 0) { doneMorph = true; state = MorphState.ball; }
        }


    }
}
