using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
	[Header("Devil")]
	[SerializeField] public Sprite sprite;
	public string name;
	public string sobriquet;

	[Header("Texts")]
	[TextArea(5, 40)] public string inProgressText;
    [TextArea(5, 40)] public string completedText;

    [Header("Stats")]
	public SinType[] requestedType;
	public int[] requestedQuantity;
	public int reward;
	public bool inProgress = true;
}
