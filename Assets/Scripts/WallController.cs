using UnityEngine;

public class WallController : MonoBehaviour
{
    public Sprite maskSprite;
    
    private SpriteRenderer _wallRenderer;
    private SpriteMask _wallMask;

    private void OnValidate()
    {
        Setup();
    }

    public void Setup()
    {
        if (!_wallMask) _wallMask = GetComponentInChildren<SpriteMask>();
        if (!_wallRenderer) _wallRenderer = GetComponentInChildren<SpriteRenderer>();

        _wallMask.sprite = maskSprite;
        _wallRenderer.maskInteraction = maskSprite ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;
    }
}