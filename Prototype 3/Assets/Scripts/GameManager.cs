using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float score;
    private PlayerController playerController;
    public Transform startingPoint;
    public Text text;
    public float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //instantiate a game Object Script file
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        playerController.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.gameOver)
        {
            if (playerController.doubleSpeed)
            {
                score += 2;
            }
            else{
                score++;
            }
            Debug.Log("Score : " + score);
            text.text = "Score : "+ score;
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerController.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier",
        0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerController.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }
        playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier",
        1.0f);
        playerController.gameOver = false;
    }
}
