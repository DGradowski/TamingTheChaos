using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialDialogue : MonoBehaviour
{
    [TextArea(5,10)]
    [SerializeField] string[] dialogues;
    [SerializeField] TMPro.TextMeshProUGUI text;
    int dialogueNumber = 0;
    float timer = 0f;
    [SerializeField] float speed = 0f;
    [SerializeField] GameObject[] panels;
    // Start is called before the first frame update
    void Start()
    {
        dialogueNumber = 0;
        text.text = "";
		for (int i = 0; i < panels.Length; i++)
		{
			panels[i].SetActive(false);
		}
		panels[dialogueNumber].SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        if (dialogueNumber >= dialogues.Length) return;
        if ((int)timer < dialogues[dialogueNumber].Length)
        {
            timer += Time.deltaTime * speed;
            text.text = "";

            for (int i = 0; i < (int)timer; i++)
            {
                text.text += dialogues[dialogueNumber][i];
            }
        }
        else
        {
            text.text = dialogues[dialogueNumber];
        }
    }

    public void LoadNextDialogue()
    {
		if (dialogueNumber >= dialogues.Length) return;
		if ((int)timer >= dialogues[dialogueNumber].Length)
        {
			dialogueNumber++;
            if (dialogueNumber >= dialogues.Length) SceneManager.LoadScene(2);
			if (dialogueNumber >= dialogues.Length) return;
			timer = 0f;
			text.text = "";
			for (int i = 0; i < panels.Length; i++)
			{
				panels[i].SetActive(false);
			}
			panels[dialogueNumber].SetActive(true);
		}
        else
        {
            timer = dialogues[dialogueNumber].Length;
        }
        
    }
}
