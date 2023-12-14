using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
public class FloorTriggerController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
 
    [Header("On Trigger Enter")]
    public UnityEvent onTriggerEnterActions;
    public bool delayOnTriggerEnterActions;
    public float delayForOnTriggerEnterActions = 0.0f;
 
    [Header("On Trigger Exit")]
    public UnityEvent onTriggerExitActions;
    public bool delayOnTriggerExitActions;
    public float delayForOnTriggerExitActions = 0.0f;
 
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (delayOnTriggerEnterActions) StartCoroutine(InvokeActionsWhenTriggerEntered());
            else onTriggerEnterActions.Invoke();
        }
    }
 
    private IEnumerator InvokeActionsWhenTriggerEntered()
    {
        yield return new WaitForSeconds(delayForOnTriggerEnterActions);
        onTriggerEnterActions.Invoke();
    }
 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (delayOnTriggerExitActions) StartCoroutine(InvokeActionsWhenTriggerExited());
            else onTriggerExitActions.Invoke();
        }
    }
 
    private IEnumerator InvokeActionsWhenTriggerExited()
    {
        yield return new WaitForSeconds(delayForOnTriggerExitActions);
        onTriggerExitActions.Invoke();
    }
}