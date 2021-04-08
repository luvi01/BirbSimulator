using UnityEngine;
using System.Collections;

public class MoveTube : MonoBehaviour
{
    public float tubeSpeed;
    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.GetInstance();

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME &
             gm.gameState != GameManager.GameState.RESUME) return;

        tubeSpeed = 5;

        transform.position += Vector3.left * tubeSpeed * Time.deltaTime;
    }
}
