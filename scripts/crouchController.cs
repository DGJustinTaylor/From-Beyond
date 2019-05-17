using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchController : MonoBehaviour
{
    public BoxCollider2D standing;
    public BoxCollider2D crouching;

    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        standing.enabled = true;
        crouching.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<playerController>().isGrounded == false)
        {
            standing.enabled = true;
            crouching.enabled = false;
        }
        else
        {
            if(Player.GetComponent<playerController>().isCrouching == true)
            {
                standing.enabled = false;
                crouching.enabled = true;
            }
            else
            {
                standing.enabled = true;
                crouching.enabled = false;
            }
        }
    }
}
