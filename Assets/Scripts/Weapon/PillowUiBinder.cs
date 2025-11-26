using UnityEngine;

public class PillowUiBinder : MonoBehaviour
{
    private PillowUI _uI;
    private PillowThrowHandler _handler;

    public void SetUI(PillowUI uI)
    {
        _uI = uI;
    }

    public void SetHandler(PillowThrowHandler handler)
    {
        _handler = handler;
    }

    public void Bind()
    {
        _handler.OnPillowAdded += _uI.SpawnIcons;
        _handler.OnThrow += _uI.HidePillow;
        _handler.OnRecall += _uI.ShowPillow;
    }

    public void Unbind()
    {
        _handler.OnPillowAdded -= _uI.SpawnIcons;
        _handler.OnThrow -= _uI.HidePillow;
        _handler.OnRecall -= _uI.ShowPillow;
    }
}
