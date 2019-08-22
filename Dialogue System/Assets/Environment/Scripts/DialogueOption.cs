using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption {
    [TextArea(1, 1)] public string optionButtonText;
    [TextArea(3, 10)] public string optionText;
}
