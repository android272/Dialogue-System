using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : Node {
[Input] public Node input;
	public string NPCName;
	[TextArea(3, 10)] public string dialogueText;
	[Output(dynamicPortList = true)] public DialogueOption[] dialogueOptions;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}