using System.Collections;
using UnityEngine;

public interface ITransitionStrategy 
{
    IEnumerator StartTransition(TransitionManager manager,string fromScene, string toScene);
}
