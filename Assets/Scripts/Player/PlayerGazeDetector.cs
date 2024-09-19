using Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerRangeDetector))]
public class PlayerGazeDetector : MonoBehaviour
{
    private PlayerStateManager stateManager;

    private PlayerRangeDetector rangeDetector;
    private void Start() => rangeDetector = GetComponent<PlayerRangeDetector>();

    [Inject]
    public void Constructor(PlayerStateManager stateManager) => this.stateManager = stateManager;

    private void FixedUpdate()
    {
        IEnumerable<Collider> inRange = rangeDetector.GetCollidersInRange();
        bool foundDamagable = inRange.Any(collider => collider.TryGetComponent(out IDamageable _));

        if (!foundDamagable && stateManager.GazeState == PlayerStateManager.PlayerGazeState.Damagable)
            stateManager.GazeState = PlayerStateManager.PlayerGazeState.None;

        if (foundDamagable && stateManager.GazeState == PlayerStateManager.PlayerGazeState.None)
            stateManager.GazeState = PlayerStateManager.PlayerGazeState.Damagable;
    }
}
