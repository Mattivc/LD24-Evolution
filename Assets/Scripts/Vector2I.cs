using UnityEngine;
using System.Collections;

public struct Vector2I {
	
	public int x;
	public int y;
	
	public Vector2I(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	
	public static Vector2I Zero
	{
		get { return new Vector2I(0,0); }
	}
	
	public static Vector2I One
	{
		get { return new Vector2I(1,1); }
	}
	
	public string ToString()
	{
		return this.x.ToString() + " - " + this.y.ToString();
	}
	
}
