using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [SerializeField] private Transform PuzzleBox;
    [SerializeField] private List<Transform> tangrams;
    private Transform tangram;


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
        tangram = null;
        SpawnTanagram();
    }

    public void CheckFinish()
    {
        if (PuzzleBox.childCount > 0)
            return;

        foreach (Transform t in tangram)
        {
            // make sure no shapes are stacked
            // check slot match
            if (t.childCount != 1 || (t.GetComponent<Shape>().CheckMatch() == false)){
                return;
            }
        }

        SpawnTanagram();
    }

    private void SpawnTanagram()
    {
        if (null != tangram)
        {
            ResetChildren();
            Destroy(tangram.gameObject);
        }

        if (tangrams.Count > 0)
        {
            int i = Random.Range(0, tangrams.Count);
            tangram = Instantiate(tangrams[i], transform);
            tangrams.RemoveAt(i);
        }
        else
        {
            GameOver();
        }
    }

    private void ResetChildren()
    {
        foreach (Transform t in tangram)
        {
            foreach (Transform c in t)
            {
                c.GetComponent<Piece>().Respawn();
            }
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
