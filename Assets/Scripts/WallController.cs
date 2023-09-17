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
        if (MurosManager.instance.gameOver) return;
        if (MurosManager.instance.IsHolding) distanceToMove = 0;
        else distanceToMove = MurosManager.instance.GameSpeed;
        if (MurosManager.instance.IsWallping) distanceToMove = MurosManager.instance.GameSpeed * MurosManager.instance.WallpingSpeed;
        transform.position += new Vector3(0, distanceToMove, 0);
    }
}