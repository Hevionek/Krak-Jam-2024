using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.Universal;

public enum Face
{
    Neutral,
    Damage,
    Attack,
    Dead,
}

[RequireComponent (typeof(DecalProjector))]
public class FaceController : MonoBehaviour
{
    private DecalProjector _decalProjector;
    public DecalProjector DecalProjector
    {
        get 
        {
            if (_decalProjector == null)
                _decalProjector = GetComponent<DecalProjector>();
            return _decalProjector; 
        }
    }
    private void OnEnable()
    {
        DecalProjector.enabled = true;
    }

    public void SetFace(Face face)
    {
        int faceInt = (int)face;
        float horizontalOffset = (faceInt % 2) * 0.5f;
        float verticalOffset = 0.5f - (faceInt / 2) * 0.5f;

        DecalProjector.uvScale = 0.5f * Vector2.one;
        DecalProjector.uvBias = new Vector2(horizontalOffset, verticalOffset);
    }

    [Button]
    public void SetRandomFace()
    {
        SetFace((Face)Random.Range(0, 4));
    }
}
