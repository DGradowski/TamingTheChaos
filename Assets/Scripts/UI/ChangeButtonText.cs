using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonText : Button
{
	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		var targetColor =
			state == SelectionState.Disabled ? colors.disabledColor :
			state == SelectionState.Highlighted ? colors.highlightedColor :
			state == SelectionState.Normal ? colors.normalColor :
			state == SelectionState.Pressed ? colors.pressedColor :
			state == SelectionState.Selected ? colors.selectedColor : Color.white;

		foreach (var graphic in GetComponentsInChildren<Graphic>())
		{
			graphic.CrossFadeColor(targetColor, 0f, true, true);
		}
	}
}
