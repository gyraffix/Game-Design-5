using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private Dictionary<string, Vector3[]> drawings;

    #region Getters
    public static GameManager Instance { get => instance; }

    #endregion

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void AddDrawing(string drawingName, Vector3[] drawing)
    {
        drawings.Add(drawingName, drawing);
    }
    public void RemoveDrawing(string drawingName)
    {
        drawings.Remove(drawingName);
    }
}
