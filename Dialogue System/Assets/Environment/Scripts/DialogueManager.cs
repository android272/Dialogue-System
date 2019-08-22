using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	[HideInInspector] public DialogueGraph dialogue;
	[HideInInspector] public DialogueNode currentDialogueNode;
	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	public GameObject buttonContainer;
	public GameObject buttonPrefab;

	public void StartDialogue (DialogueGraph dialogue) {
		ClearButtons();
		this.dialogue = dialogue;
        currentDialogueNode = (DialogueNode) dialogue.GetStartNode();

		animator.SetBool("IsOpen", true);

		nameText.text = currentDialogueNode.NPCName;

		DisplayNextSentence();
	}

	public void DisplayNextSentence () {
		StopAllCoroutines();
		StartCoroutine(TypeSentence(currentDialogueNode.DialogueText));
		DisplayOptionButtons();
	}


	IEnumerator TypeSentence (string sentence) {
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	private void DisplayOptionButtons() {
		if(currentDialogueNode.GetPort("dialogueOptions 0") == null) {
			var exitButton = CreateButton("exit conversation", buttonContainer);
			exitButton.onClick.AddListener(() => EndDialogue());
		}
		
		for(int option = 0; option < currentDialogueNode.dialogueOptions.Length; option++) {
            int number = option;
            DialogueOption optionInDialog = currentDialogueNode.dialogueOptions[option];
			var button = CreateButton(currentDialogueNode.dialogueOptions[option].optionButtonText, buttonContainer);
			button.onClick.AddListener(() => AcceptButtonInput(optionInDialog, number));
        }
	}

	private Button CreateButton(string buttonText, GameObject buttonGroup) {
            GameObject newButton = Instantiate(buttonPrefab) as GameObject;
            newButton.GetComponentInChildren<Text>().text = buttonText;
            newButton.transform.SetParent(buttonGroup.transform, false);
            return newButton.GetComponent<UnityEngine.UI.Button>();
    }

	void AcceptButtonInput(DialogueOption option, int optionNumber) {
		if(currentDialogueNode.GetPort($"dialogueOptions {optionNumber}").Connection != null) {
            currentDialogueNode = (DialogueNode) currentDialogueNode.GetPort($"dialogueOptions {optionNumber}").Connection.node;
			ClearButtons();
			DisplayNextSentence();
        } else {
			ClearButtons();
            EndDialogue();
        }
    }

	void ClearButtons() {
        foreach(Transform child in buttonContainer.transform) {
            Destroy(child.gameObject);
        }
    }

	void EndDialogue() {
		animator.SetBool("IsOpen", false);
	}
}
