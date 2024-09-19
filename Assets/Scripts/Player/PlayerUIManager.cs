using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Image hitIndicator;

    [Inject]
    public void Constructor(PlayerStateManager stateManager)
        => stateManager.OnPlayerGazeStateChange += (_, e) => hitIndicator.enabled = e.newState == PlayerStateManager.PlayerGazeState.Damagable;
}
