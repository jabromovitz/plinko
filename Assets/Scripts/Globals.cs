using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

	public enum Team { NONE, LEFT, RIGHT }
	public static Color[] TEAM_COLORS = new Color[] { Color.white, Color.blue, Color.red};
	
	// Hands
	public static float PEG_PULL_TIME = 1.5f;

	// Puck Spawner
	public const int BALLS_PER_DROP = 15;
	public const float BALL_DROP_FREQ = 0.5f;
	public const float MAX_BALL_DROPPER_SPEED = 30;
	public const float BALL_DROPPER_WAIT_TIME = 3.0f;
	public const float TIME_BETWEEN_BALL_DROPS = 0.25f;

	// Scene Manager
	public static float PEG_REGEN_TIME = 20.0f;
}
