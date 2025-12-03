using UnityEngine;
using UnityEngine.UI;

public class UiParticleSystem : MaskableGraphic
{
    [SerializeField] private ParticleSystemRenderer _renderer;
    [SerializeField] private Texture _texture;
    [SerializeField] private Camera _bakeCamera;

    public override Texture mainTexture => _texture ?? base.mainTexture;
    private void Update()
    {
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(Mesh mesh)
    {
        mesh.Clear();

        if (_renderer != null & _bakeCamera != null)
        {
            _renderer.BakeMesh(mesh, _bakeCamera);
        }
    }
}
