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
	
	public static Vector2I Rigth
	{
		get { return new Vector2I(1,0); }
	}
	
	public static Vector2I Up
	{
		get { return new Vector2I(0,1); }
	}
	
	public static Vector2I operator +(Vector2I v1, Vector2I v2)
	{
		return new Vector2I(v1.x + v2.x, v1.y + v2.y);
	}
	
	public static Vector2I operator -(Vector2I v1, Vector2I v2)
	{
		return new Vector2I(v1.x - v2.x, v1.y - v2.y);
	}
	
	public override string ToString()
	{
		return this.x.ToString() + " - " + this.y.ToString();
	}
	
}
