using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MurosManager : MonoBehaviour
{

    public static MurosManager instance;

    [SerializeField] private GameObject[] walls = Array.Empty<GameObject>();
    [SerializeField] private float timeCounter = 0;
    [SerializeField]private float timeBetweenWalls;
    [SerializeField] private float wallpingSpeed = 3;


    [SerializeField]private bool isWallping = false;
    [SerializeField]private bool isHolding = false;
    private float distanceToMove;

    public bool IsWallping { get => isWallping; set => isWallping = value; }
    public bool IsHolding { get => isHolding; set => isHolding = value; }
    public float WallpingSpeed { get => wallpingSpeed; set => wallpingSpeed = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        distanceToMove = -0.02f;
    }


    void FixedUpdate()
    {
        if(!isHolding)timeCounter += 0.05f * (isWallping?WallpingSpeed:1f);
        if (timeCounter >= timeBetweenWalls)
        {
            timeCounter = 0;
            var randomIndex = Random.Range(0, walls.Length);
            Instantiate(walls[randomIndex], transform);
        }



    }
}
