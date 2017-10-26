using UnityEngine;

public abstract class State : MonoBehaviour {

    private LogicManager logicManager;
    protected LogicManager LogicManager { get { return logicManager; } set { logicManager = value; } }
    
    /// <summary>
    /// Called when entering the state
    /// </summary>
    /// <param name="logicManager">Logicmanager calling and managing this state</param>
    public virtual void OnEnterState(LogicManager logicManager)
    {
        this.logicManager = logicManager;
    }

    /// <summary>
    /// Called when leaving the state.
    /// </summary>
    protected abstract void OnLeaveState();
}
