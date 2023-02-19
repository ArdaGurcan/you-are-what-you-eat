using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
  public TextAsset textFile;
  [SerializeField]
  string[] dialogues;
  Text text;
  PlayerMovement player;
  string playerText = "";
  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    text = GetComponent<Text>();
    dialogues = textFile.text.Split(
        new string[] { "\r\n", "\r", "\n" },
        System.StringSplitOptions.None
        );
    InvokeRepeating("NewDialogue", 0f, 10f);
  }


void Update()
{
  if(!playerText.Equals(PlayerMovement.priorityDialogue)){
    playerText = PlayerMovement.priorityDialogue;
    NewDialogue();
  }
}
  void NewDialogue()
  {
    if (playerText.Equals(""))
      text.text = dialogues[Random.Range(0, dialogues.Length)];
    else
      text.text = PlayerMovement.priorityDialogue;
  }
}
