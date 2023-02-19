using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scapel : MonoBehaviour
{
    PlayerMovement player;
    private bool inScapelRange = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inScapelRange && Input.GetKeyDown(KeyCode.X)) {
            PlayerMovement.hasScapel = true;
            StartCoroutine(slaayyy())
;        }
        if(PlayerMovement.hasScapel) {
            this.enabled = false;
            gameObject.SetActive(false);
        }

  
    }

    IEnumerator slaayyy()
    {
        player.priorityDialogue = "SLAAYYYY";
        yield return new WaitForSeconds(10f);
        player.priorityDialogue = "";

    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Player") && PlayerMovement.hasScapel == false)
           inScapelRange = true;
    }

   private void OnCollisionExit2D(Collision2D other) 
   {
        if(other.collider.CompareTag("Player") && PlayerMovement.hasScapel == false)
            inScapelRange = false;
    }
}
