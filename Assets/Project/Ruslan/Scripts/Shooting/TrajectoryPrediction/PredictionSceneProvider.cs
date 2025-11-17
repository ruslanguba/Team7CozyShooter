using UnityEngine;
using UnityEngine.SceneManagement;


public class PredictionSceneProvider
{
    private Scene _scene;
    private PhysicsScene _physicsScene;

    public PhysicsScene PhysicsScene => _physicsScene;
    public Scene Scene => _scene;

    public void EnsureSceneCreated()
    {
        if (_scene.IsValid()) return;

        // —оздаЄм отдельную сцену с физикой 3D
        _scene = SceneManager.CreateScene("PredictionScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _scene.GetPhysicsScene();

        // ¬ключаем ручную симул€цию Physics
        Physics.simulationMode = SimulationMode.Script;
        Physics.Simulate(Time.fixedDeltaTime);
    }
}
