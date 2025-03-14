using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [SerializeField] private Transform PuzzleBox;
    [SerializeField] private List<Transform> tanagrams;
    private Transform tanagram;


    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        SpawnTanagram();
    }

    public void CheckFinish()
    {
        if (PuzzleBox.childCount > 0)
            return;

        foreach (Transform t in tanagram)
        {
            // make sure no shapes are stacked
            // check slot match
            bool b = t.GetComponent<Shape>().CheckMatch();
            Debug.Log($"{t.gameObject.name}: {b}");
            if (t.childCount != 1 || (t.GetComponent<Shape>().CheckMatch() == false)){
                Debug.Log("not complete");
                return;
            }
        }

        SpawnTanagram();
    }

    private void SpawnTanagram()
    {
        Debug.Log("New gram");
        if (tanagrams.Count > 0)
        {
            int i = Random.Range(0, tanagrams.Count);
            tanagram = Instantiate(tanagrams[i], transform);
            tanagrams.RemoveAt(i);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over, You Win!");
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Application.Quit();
    }
}
