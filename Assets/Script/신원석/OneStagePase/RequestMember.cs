using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Pattern
{ 
    one,
    two, 
    three,
    four, 
    five,
}

public class SpawnManager : MonoBehaviour, PatternChoiceInterface
{
    Pattern patternEnum = Pattern.one;
    Coroutine coroutine;

    public void SetPattern(int value)
    {
        patternEnum = (Pattern)value;
    }

    private Dictionary<Pattern, Func<IEnumerator>> patternActions;

    void Start()
    {
        patternActions = new Dictionary<Pattern, Func<IEnumerator>>
        {
            { Pattern.one, PatternOneCoroutine },
            { Pattern.two, PatternTwoCoroutine },
            { Pattern.three, PatternThreeCoroutine }
        };
    }
    void Update()
    {
        if (coroutine == null && patternActions.ContainsKey(patternEnum))
        {
            coroutine = StartCoroutine(patternActions[patternEnum]());
        }
    }
    IEnumerator PatternOneCoroutine()
    {
        Debug.Log("패턴 1 실행!");
        yield break;
    }

    IEnumerator PatternTwoCoroutine()
    {
        Debug.Log("패턴 2 실행!");
        yield break;
    }
    IEnumerator PatternThreeCoroutine()
    {
        Debug.Log("패턴 3 실행!");
        yield break;
    }

}
