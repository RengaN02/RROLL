using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance {
        get {
            if (_instance == null) {
                _instance = FindFirstObjectByType<TimeManager>();
            }
            return _instance;
        }
    }
    private static TimeManager _instance;

    public List<RenBehaviour> objects = new List<RenBehaviour>();

    public float deltaTimeMultiplier = 1f;
    public float fixedDeltaTimeMultiplier = 1f;

    public static float deltaTime 
    {
        get 
        {
            if (Instance != null) return Time.deltaTime * Instance.deltaTimeMultiplier;
            
            return Time.deltaTime;
        }
    }

    public static float fixedDeltaTime 
    {
        get 
        {
            if (Instance != null) return Time.fixedDeltaTime * Instance.fixedDeltaTimeMultiplier;
            
            return Time.fixedDeltaTime;
        }
    }

    void Awake() {
        _instance = this;
    }

    void Update() {

        for (int i = objects.Count - 1; i >= 0; i--) {
            if (objects[i] != null) {
                RenBehaviour script = objects[i];
                script.Tick(deltaTime * script.timeScale);
            } else {
                objects.RemoveAt(i);
            }
        }
    }

    void FixedUpdate()
    {

        for (int i = objects.Count - 1; i >= 0; i--) {
            if (objects[i] != null) {
                RenBehaviour script = objects[i];
                script.FixedTick(fixedDeltaTime * script.fixedTimeScale);
            } else {
                objects.RemoveAt(i);
            }
        }
    }

    public void Register(RenBehaviour obj) => objects.Add(obj);
    public void Unregister(RenBehaviour obj) => objects.Remove(obj);
}