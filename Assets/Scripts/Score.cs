using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] private float distanceToMove;
    private float score;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (MurosManager.instance.gameOver) return;
        if (MurosManager.instance.IsHolding) distanceToMove = 0;
        else distanceToMove = MurosManager.instance.GameSpeed;
        if (MurosManager.instance.IsWallping) distanceToMove = MurosManager.instance.GameSpeed * MurosManager.instance.WallpingSpeed;
        score -= distanceToMove*100;
        GetComponent<TMP_Text>().text = (score).ToString();
        PlayerPrefs.SetInt("score", (int)score);
    }
}
