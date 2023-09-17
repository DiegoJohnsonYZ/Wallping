using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroController : MonoBehaviour
{
    [SerializeField]float distanceToMove;

    private void Start()
    {
        ResizeSpriteToScreen();
    }
    void Update()
    {
        if (MurosManager.instance.IsHolding) distanceToMove = 0;
        else distanceToMove = MurosManager.instance.GameSpeed;
        if (MurosManager.instance.IsWallping) distanceToMove = MurosManager.instance.GameSpeed * MurosManager.instance.WallpingSpeed;
        transform.position += new Vector3(0, distanceToMove, 0);


    }

    public float ResizeSpriteToScreen()
    {
        var sr = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenWidth / width, 0);
        return height * (worldScreenWidth / width);
    }
}
