using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
	public static CursorManager instance; 
    public Texture2D normalCursor;
    public Vector2 normalCursorHotspot;

    public Texture2D hoverCursor;
    public Vector2 hoverCursorHotspot;

	private void Awake()
	{
		instance = this;
		SetNormalCursor();
	}

	public void SetNormalCursor()
	{
		Cursor.SetCursor(normalCursor, normalCursorHotspot, CursorMode.Auto);
	}

	public void SetHoverCursor()
	{
		Cursor.SetCursor(hoverCursor, hoverCursorHotspot, CursorMode.Auto);
	}
}
