using UnityEditor;
using UnityEngine;

public class RoamerStateMachine : MonoBehaviour
{
    public BasicRoamerState startingState;
    public BasicRoamerState currentState;

    private void Awake()
    {
        Initialize();
        currentState = startingState;
        startingState.Enter();
    }
    public void Initialize()
    {
        BasicRoamerState[] states = GetComponents<BasicRoamerState>();
        foreach (BasicRoamerState state in states)
        {
            state.Initialize(this);
        }
    }

    public void ChangeState(BasicRoamerState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

}