using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuilleScript : MonoBehaviour
{
    public bool isStanding = true;
    
    void Update()
    {
        if (!isStanding && shouldCountPoints)
        {
            AddPoint();
        }

    }

    private void AddPoint()
    {
        ScoreScript.Instance.score++;
        UIManager.Instance._scoreText.text = ScoreScript.Instance.score.ToString();
        shouldCountPoints = false;
        StartCoroutine(DisableQuille());
        
    }

    private IEnumerator DisableQuille()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public bool shouldCountPoints = true;
}
