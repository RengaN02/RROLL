using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : RenBehaviour
{

    public static GameManager instance;

    public LayerMask enemyLayers;


    public float defaultTickPerSecond;

    [Header("StartingEffect")]
    public float SEstartValue = 0f;
    public float SEtargetValue = 16f;
    public float SEtargetTime = 3f;
    public Material posterized;

    [Header("AI")]
    public List<NavMeshAgent> meshAgents = new List<NavMeshAgent>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            meshAgents = new List<NavMeshAgent>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void StartEpisode()
    {
        StartCoroutine(StartGame());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        posterized.SetFloat("_Steps", SEtargetValue);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        posterized.SetFloat("_Steps", SEstartValue);
        StartCoroutine(PrepareNewSceneRoutine());
    }

    IEnumerator PrepareNewSceneRoutine()
    {
        meshAgents.Clear();

        yield return new WaitForEndOfFrame(); 

        NavMeshAgent[] foundAgents = FindObjectsOfType<NavMeshAgent>();
        meshAgents.AddRange(foundAgents);

        StartEpisode();
    }

    IEnumerator StartGame()
    {
        yield return StartCoroutine(StartEffect());
        yield return new WaitForSeconds(0.25f);
        foreach (var agent in meshAgents)
        {
            agent.enabled = true;
        }
    }

    IEnumerator StartEffect()
    {
        float timePassed = 0f;
        
        while (timePassed < SEtargetTime)
        {
            timePassed += TimeManager.deltaTime * timeScale;
            
            posterized.SetFloat("_Steps", Mathf.Lerp(SEstartValue, SEtargetValue, timePassed / SEtargetTime));
            
            yield return null; 
        }

        posterized.SetFloat("_Steps", SEtargetValue);
    }

    public void DoNothing()
    {
        
    }

}
