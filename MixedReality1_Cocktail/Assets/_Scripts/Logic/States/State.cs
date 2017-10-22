using UnityEngine;

public abstract class State : MonoBehaviour {

    private LogicManager logicManager;
    protected LogicManager LogicManager { get { return logicManager; } set { logicManager = value; } }
    
    public virtual void OnEnter(LogicManager logicManager)
    {
        this.logicManager = logicManager;
    }
}
