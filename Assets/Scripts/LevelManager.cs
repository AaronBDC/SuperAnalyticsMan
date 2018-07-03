using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum State_LevelManager
    {
        Intro,
        Playing,
        Completed
    }
    public static LevelManager Instance;
    public State_LevelManager State_Current;
    public string NextLevelName;

    internal void SetState(State_LevelManager newState)
    {
        State_Current = newState;
    }

    // Use this for initialization
    void Start () {
        Instance = this;
        State_Current = State_LevelManager.Intro;
	}
	
	// Update is called once per frame
	void Update () {
        switch (State_Current)
        {
            case State_LevelManager.Intro:
                if (Input.GetKeyDown(KeyCode.Space))
                    State_Current = State_LevelManager.Playing;
                break;
            case State_LevelManager.Playing:
                break;

            case State_LevelManager.Completed:
                if (Input.GetKeyDown(KeyCode.Space))
                    SceneManager.LoadScene(NextLevelName);
                break;
        }
	}
}
