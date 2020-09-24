using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    
    public virtual void HandleStart() { }
    public virtual void HandleExit() { }

    //Update for the states. Use this instead of Update()
    public virtual EnemyState StateUpdate() { return null; }

    //Fixed Update for the states. Use this instead of FixedUpdate()
    public virtual void StateFixedUpdate() {}
 
   
}
