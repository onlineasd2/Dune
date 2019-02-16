using UnityEngine;
using UnityEngine.EventSystems;

public class HandleScript : MonoBehaviour
{
    public ScoreManager scoreManager;

    public void OnMouseDown ()
    {
        scoreManager.startGame = true;
        Debug.Log("Start");
    }

}