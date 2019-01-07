﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager S_INSTANCE = null;

    public static GameManager INSTANCE {
        get {
            if (S_INSTANCE == null) {
                S_INSTANCE = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            if (S_INSTANCE == null) {
                GameObject obj = new GameObject("AutoGenerated: GameManager");
                S_INSTANCE = obj.AddComponent(typeof(GameManager)) as GameManager;
                Debug.LogError("Could not locate an GameManager object. \n GameManager was Generated Automatically.");
            }

            return S_INSTANCE;
        }
    }

    [HideInInspector]
    public float value;

    public List<GameObject> gameObjects = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> tags = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> artifactsFound = new List<GameObject>();

    public GameObject canvas;

    public Tutorial tutorial;
    public VideoPlayer introPlayer;

    public float[] introPauseTimes;

    public bool watchingIntro = true;

    private void Awake () {
        tutorial = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Tutorial>();
    }

    private void Start () {
        canvas.SetActive(false);
    }

    int pauseIndex = 0;
    private void Update () {

        if (introPlayer.time >= introPauseTimes[pauseIndex]) {
            introPlayer.Pause();
        } else if (!introPlayer.isPlaying && introPlayer.time <= 3.0f) {
            introPlayer.Play();
        }

        if (!introPlayer.isPlaying && introPlayer.time >= 4.0f) {
            canvas.SetActive(true);
            watchingIntro = false;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (watchingIntro) {
                if (!introPlayer.isPlaying) {
                    pauseIndex++;
                }
            }
        }

    }

    public void StartGame () {
        foreach (GameObject gameObject in gameObjects) {
            gameObject.SetActive(true);
        }

        tutorial.active = false;
        tutorial.gameObject.SetActive(false);
    }

    public void RestartGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame () {
        Application.Quit();
    }



}