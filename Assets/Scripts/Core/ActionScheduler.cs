
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{

    IAction currentAction;
    // Start is called before the first frame update
    public void StartAction(IAction action)
    {
        if (currentAction == action) return;
        Debug.Log("New action");
        if (currentAction != null)
        {
            Debug.Log("Cancel");
            currentAction.Cancel();
        }
        currentAction = action;
    }
}
