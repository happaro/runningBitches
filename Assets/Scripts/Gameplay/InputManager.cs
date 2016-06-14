using UnityEngine;
using System.Collections;

public enum AxisType {Horizontal, Vertical}
public class InputManager : MonoBehaviour 
{
	public static bool GetCurrentJoyButton(int joyNum, ButtonNum buttonNum)
	{
		return Input.GetButtonDown(string.Format("joy{0}_{1}", joyNum, buttonNum.ToString().ToLower()));
	}

	public static float GetCurrentJoyAxis(int joyNum, AxisType axisType)
	{
		return Input.GetAxis(string.Format("joy{0}_{1}", joyNum, axisType.ToString().ToLower()));
	}
}
