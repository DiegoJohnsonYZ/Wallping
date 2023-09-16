using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurosManager : MonoBehaviour
{

    public static MurosManager instance;

    [SerializeField] private GameObject muro;
    [SerializeField] private float timeCounter = 0;
    [SerializeField]private float timeBetweenWalls;
    [SerializeField] private float wallpingSpeed = 3;


    [SerializeField]private bool isWallping = false;
    [SerializeField]private bool isHolding = false;


    [SerializeField]private float gameSpeed = -0.01f;

    public bool IsWallping { get => isWallping; set => isWallping = value; }
    public bool IsHolding { get => isHolding; set => isHolding = value; }
    public float WallpingSpeed { get => wallpingSpeed; set => wallpingSpeed = value; }
    public float GameSpeed { get => gameSpeed; set => gameSpeed = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }


    void FixedUpdate()
    {
        if(!isHolding)timeCounter += 0.05f * (isWallping?WallpingSpeed:1f);
        if (timeCounter >= timeBetweenWalls)
        {
            timeCounter = 0;
            Instantiate(muro, transform);
        }



    }
}
