using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoController : MonoBehaviour
{
    [SerializeField] float distanceToMove;

    private void Start()
    {
        ResizeSpriteToScreen();
    }
    void Update()
    {
        if (MurosManager.instance.IsHolding) distanceToMove = 0;
        else distanceToMove = -0.01f;
        if (MurosManager.instance.IsWallping) distanceToMove = -0.01f * MurosManager.instance.WallpingSpeed;
        transform.position += new Vector3(0, distanceToMove, 0);
        if (transform.localPosition.y <= FondoManager.instance.SizeY*-4)
        {
            transform.localPosition = new Vector3(transform.position.x, 0, transform.position.z);
            StartCoroutine(TrackBegin());
            FondoManager.instance.SetSprite(this.gameObject);
        }
    }

    private IEnumerator TrackBegin() {
        float elapsedTime = 0;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition =new Vector3(0, FondoManager.instance.PrimerFondo.transform.localPosition.y, 0) + new Vector3(0,FondoManager.instance.SizeY,0);
            yield return null;
        }
        FondoManager.instance.PrimerFondo = this;
    }

    public float ResizeSpriteToScreen()
    {
        var sr = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3( worldScreenWidth / width, worldScreenWidth / width,0);
        return height * (worldScreenWidth / width);
    }
}
