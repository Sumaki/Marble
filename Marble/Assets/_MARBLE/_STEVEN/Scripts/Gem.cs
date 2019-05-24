using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
   // public Animator ani;
    public Material newElementMaterial;
    public GameObject humanoid_mesh;
    public GameObject humanoid_core_mesh;
    public GameObject ball_mesh;
    public enum Element { Ruby, Sapphire, Amethyste}
    public Element whichElement;
    GameObject gm_; // GameManager


    // Floating Gem
    public float degreePerSecond;
    public float amplitude;
    public float frequency;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBall" || other.gameObject.tag == "PlayerHumanoid")
        {

            Debug.Log("Obtained: " + gameObject.name);
            CheckWhatElementObtained();
            StartCoroutine(TimerToSwtich());
            //ani.SetTrigger("Open");
            gm_.GetComponent<GameManager>().playerHumanoid.GetComponent<CharacterAnimationState>().state = CharacterAnimationState.CharacterState.grab_element;
            Destroy(this.gameObject, 0.5f);
        }
    }

    IEnumerator TimerToSwtich()
    {
        yield return new WaitForSeconds(0.4f);
        humanoid_mesh.GetComponent<SkinnedMeshRenderer>().material = newElementMaterial;
        ball_mesh.GetComponent<SkinnedMeshRenderer>().material = newElementMaterial;
        humanoid_core_mesh.GetComponent<SkinnedMeshRenderer>().material = newElementMaterial;

    }

    void Start()
    {
        gm_ = GameObject.FindGameObjectWithTag("GameManager");
        posOffset = transform.position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreePerSecond, 0f), Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    void CheckWhatElementObtained()
    {
        switch (whichElement)
        {
            case Element.Ruby:
                gm_.GetComponent<GameManager>().gotRuby = true;
                break;
            case Element.Sapphire:
                gm_.GetComponent<GameManager>().gotSapphire = true;
                break;
            case Element.Amethyste:
                gm_.GetComponent<GameManager>().gotAmethyste = true;
                break;
        }
    }
}
