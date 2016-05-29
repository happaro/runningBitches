using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager Instance;
	public List<PlayerSettings> playersSettings;
	public GameObject[] prefabs;

	private int currentLevelNum = 0;

	private bool choosePlayerScene;

	void Awake () 
	{
		playersSettings = new List<PlayerSettings>();
		if (PlayerManager.Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
		
	}

	void OnLevelWasLoaded(int levelNum)
	{
		currentLevelNum = levelNum;
		if (levelNum == 0)
			playersSettings.Clear();
	}

	void Update()
	{
		if (currentLevelNum == 0)
		{
			for (int i = 1; i <= 2; i++)
			{
				//triangle
				if (GetCurrentJoyButton(ButtonNum.Button0, i))
					AddPlayer(i);
				//triangle
				if (GetCurrentJoyButton(ButtonNum.Button9, i))
					SceneManager.LoadScene("Gameplay");
				//L1
				if (GetCurrentJoyButton(ButtonNum.Button4, i))
					ChangeColor(i, -1);
				//R1
				if (GetCurrentJoyButton(ButtonNum.Button5, i))
					ChangeColor(i, 1);
				//L2
				if (GetCurrentJoyButton(ButtonNum.Button6, i))
					ChangeSkin(i, 1);
			}
		}
		else
		{
			for (int i = 1; i <= 2; i++)
				if (GetCurrentJoyButton(ButtonNum.Button9, i))
					SceneManager.LoadScene(0);
		}
	}

	private bool GetCurrentJoyButton(ButtonNum buttonNum, int currentPlayerNum)
	{
		return Input.GetButtonDown(string.Format("joy{0}_{1}", currentPlayerNum, buttonNum.ToString().ToLower()));
	}

	private float GetCurrentJoyAxis(string axisName, int currentPlayerNum)
	{
		return Input.GetAxisRaw(string.Format("joy{0}_{1}", currentPlayerNum, axisName));
	}

	public void AddPlayer(int joyNum)
	{
		foreach (var ps in playersSettings)
			if (ps.joystickNumber.Equals(joyNum))
				return;
		playersSettings.Add(new PlayerSettings(joyNum, playersSettings.Count + 1));
		playersSettings[playersSettings.Count - 1].modelObj = Instantiate(prefabs[0], GetPosForPlayer(playersSettings[playersSettings.Count - 1].playerNumber), prefabs[0].transform.rotation) as GameObject;
	}

	private Vector3 GetPosForPlayer(int playerNum)
	{
		return new Vector3(0, 3 - 2 * (playerNum - 1), 0);
	}

	private void ChangeSkin(int joyNum, int dir)
	{
		foreach (var ps in playersSettings)
			if (ps.joystickNumber.Equals(joyNum))
			{
				var playerNum = ps.playerNumber;
				playersSettings[playerNum - 1].characterNumber = playersSettings[playerNum - 1].modelObj.GetComponent<SkinChanger>().ChangeModel(playersSettings[playerNum - 1].characterNumber + dir);
			}
	}

	private void ChangeColor(int joyNum, int dir)
	{
		foreach (var ps in playersSettings)
			if (ps.joystickNumber.Equals(joyNum))
			{
				var playerNum = ps.playerNumber;
				playersSettings[playerNum - 1].colorNumber = playersSettings[playerNum - 1].modelObj.GetComponent<SkinChanger>().ChangeMaterial(playersSettings[playerNum - 1].colorNumber + dir);
			}
	}
}

public class PlayerSettings
{
	public PlayerSettings(int joyNum, int playerNum)
	{
		joystickNumber = joyNum;
		playerNumber = playerNum;
		characterNumber = 0;
		colorNumber = 0;
	}

	public GameObject modelObj;

	public int playerNumber;
	public int joystickNumber;

	public int characterNumber;
	public int colorNumber;
}