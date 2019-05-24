using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementUnlockDoor : MonoBehaviour
{
    [Header("Which door's animator?")]
    public Animator ani_door;
    [Header("Eye Animator")]
    public Animator ani_eye;
    public enum DoorType { Ruby, Sapphire, Amethyste }
    [Header("What element does this door need?")]
    public DoorType whichElement;
    bool open = false;
    GameObject gm_;

    private void Start()
    {
        gm_ = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBall") //other.gameObject.tag == "PlayerHumanoid" || other.gameObject.tag == "PlayerBall")
        {
            CheckWhichElement();
            if (open)
            {

                // other.GetComponent<Animator>().enabled = true;
                // other.GetComponent<Animator>().SetBool("Unlock", true);
                StartCoroutine(AnimationSequence());
            }
        }
    }

    void CheckWhichElement()
    {
        switch (whichElement)
        {
            case DoorType.Ruby:
                if(gm_.GetComponent<GameManager>().gotRuby)
                    open = true;
                break;
            case DoorType.Sapphire:
                if (gm_.GetComponent<GameManager>().gotSapphire)
                    open = true;
                break;
            case DoorType.Amethyste:
                if (gm_.GetComponent<GameManager>().gotAmethyste)
                    open = true;
                break;
        }
    }

    IEnumerator AnimationSequence()
    {
        ani_eye.SetBool("EyeOpen", true);
        yield return new WaitForSeconds(3.1f);
        ani_door.SetTrigger("Open");
    }
}
