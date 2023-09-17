using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private float distanceToMove;
    [SerializeField] private Sprite maskSprite;
    [SerializeField] private SpriteRenderer wallRenderer;
    [SerializeField] private SpriteRenderer wallShadowRenderer;
    [SerializeField] private SpriteMask wallMask;
    [SerializeField] private SpriteMask shadowMask;

    private void OnValidate()
    {
        Setup();
    }

    private void Start()
    {
        ResizeSpriteToScreen(transform.GetChild(0).gameObject);
        ResizeSpriteToScreen(transform.GetChild(1).gameObject);
    }

    private void Setup()
    {
        if (!wallMask || !shadowMask) return;
        if (!wallRenderer || !wallShadowRenderer) return;

        wallMask.sprite = maskSprite;
        wallRenderer.maskInteraction = maskSprite ? SpriteMaskInteraction.VisibleOutsideMask : SpriteMaskInteraction.None;

        shadowMask.sprite = maskSprite;
        wallShadowRenderer.maskInteraction = maskSprite ? SpriteMaskInteraction.VisibleOutsideMask : SpriteMaskInteraction.None;
    }
    
    void Update()
    {
        if (MurosManager.instance.IsHolding) distanceToMove = 0;
        else distanceToMove = MurosManager.instance.GameSpeed;
        if (MurosManager.instance.IsWallping) distanceToMove = MurosManager.instance.GameSpeed * MurosManager.instance.WallpingSpeed;
        transform.position += new Vector3(0, distanceToMove, 0);
    }

    public void ResizeSpriteToScreen(GameObject obj)
    {
        var sr = obj.GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        print(width);
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = MurosManager.instance.OrtographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        float newScale = ((worldScreenWidth / width) / sr.size.x)*0.82f;

        obj.transform.localScale = new Vector3(newScale, newScale, 0);
    }
}