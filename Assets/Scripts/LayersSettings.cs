using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu]
public class LayersSettings : ScriptableObject
{
    [field: SerializeField, Layer]
    public int overgroundLayer;
    [field: SerializeField, Layer]
    public int undergroundLayer;
    [field: SerializeField, Layer]
    public int noCollisionLayer;
}
