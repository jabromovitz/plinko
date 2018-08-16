using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>Manages data for persistance between levels.</summary>
public class GameDataManager : MonoBehaviour 
{
    /// <summary>Static reference to the instance of our DataManager</summary>
    public static GameDataManager instance;

    public List<Globals.Team> playerTeams;

    /// <summary>Awake is called when the script instance is being loaded.</summary>
    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if(instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }

	void Start() {
        //Load Choose Team Scene
		SceneManager.LoadScene("ChooseTeam");
    }
}