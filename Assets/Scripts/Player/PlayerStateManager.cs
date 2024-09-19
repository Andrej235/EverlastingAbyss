using System;
using UnityEngine;

public class PlayerStateManager
{
    public enum PlayerMovementState
    {
        None = 0,
        Idle = 1,
        Walking = 2,
        Running = 4,
        InAir = 8,
    }

    private PlayerMovementState walkingState;
    public PlayerMovementState WalkingState
    {

        get => walkingState;
        set
        {
            if (value == walkingState)
                return;

            Debug.Log(value);
            walkingState = value;
        }
    }

    public enum PlayerGazeState
    {
        None = 0,
        Damagable = 1,
    }
    private PlayerGazeState gazeState;
    public PlayerGazeState GazeState
    {
        get => gazeState;
        set
        {
            if (value == gazeState)
                return;

            Debug.Log(value);
            gazeState = value;
            OnPlayerGazeStateChange?.Invoke(this, new(value));
        }
    }

    public event EventHandler<OnPlayerGazeStateChangeEventArgs> OnPlayerGazeStateChange;
    public class OnPlayerGazeStateChangeEventArgs : EventArgs
    {
        public PlayerGazeState newState;
        public OnPlayerGazeStateChangeEventArgs(PlayerGazeState newState) => this.newState = newState;
    }
}
