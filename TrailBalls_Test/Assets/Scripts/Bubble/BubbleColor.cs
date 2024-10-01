using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;

public class BubbleColor : MonoBehaviour
{
    [SerializeField] private List<Material> _materials = new List<Material>();
    private Dictionary<GameObject, Renderer> _bubblesRenderer = new Dictionary<GameObject, Renderer>();
    private Dictionary<GameObject, TrailRenderer> _bubblesTrailRenderer = new Dictionary<GameObject, TrailRenderer>();

    private System.Random _rndNumber = new System.Random();

    public IReadOnlyDictionary<GameObject, TrailRenderer> BubblesTrailRenderer => _bubblesTrailRenderer;

    public void InitColor(GameObject bubble)
    {
        _bubblesRenderer.Add(bubble, bubble.GetComponent<Renderer>());
        _bubblesTrailRenderer.Add(bubble, bubble.GetComponent <TrailRenderer>());

        ChangeColor(bubble);
    }

    public void ChangeColor(GameObject bubble)
    {
        Material material = _materials[_rndNumber.Next(0, _materials.Count - 1)];

        _bubblesRenderer[bubble].material = material;

        ChangeTrailColor(bubble, material);
    }

    private void ChangeTrailColor(GameObject bubble, Material material)
    {
        _bubblesTrailRenderer[bubble].material = material;
    }
}
