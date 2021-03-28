using System;
using System.Drawing;

public class Dot
{
	int x = 0;
	int y = 0;

	public readonly static int SIZE = 20;
	public Rectangle DotRect;

	public int X
	{
		get
		{
			return x;
		}

		set
		{
			x = value;
			DotRect.X = value;
		}
	}

	public int Y
	{
		get
		{
			return y;
		}

		set
		{
			y = value;
			DotRect.Y = value;
		}
	}

	public Dot(int x, int y)
	{
		X = x;
		Y = y;

		DotRect = new Rectangle(x, y, SIZE, SIZE);

	}
}
