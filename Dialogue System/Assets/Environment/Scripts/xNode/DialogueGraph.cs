using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph { 
	public Node GetStartNode() {
        foreach(Node node in nodes) {
            if(node.GetInputPort("input").Connection == null) { return node; }
        }

        return null;
    }
}