using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    [SerializeField]
    private Text time_left;
    [SerializeField]
    private Text score_text;
    [SerializeField]
    private Text game_over;
    
    private int score = 0;

    private int time_actual = 20;

    private player_new player;
    void Start()
    {
        player = GameObject.Find("ball").GetComponent<player_new>();

        time_left.text = "" + time_actual;
        score_text.text = "SCORE: " + score;
        game_over.gameObject.SetActive(false);
    }

    void Update()
    {
        time_left.text = "" + time_actual;
        score_text.text = "SCORE: " + score;
    }

    public void time_change()
    {
        time_actual--;
        time_left.text = "" + time_actual;

        if(time_actual == 0)
        {
            game_over.gameObject.SetActive(true);
        }
    }

    public void time_blessing()
    {
        time_actual = time_actual + 1;
    }

    public void _score()
    {
        score = score + 5;
    }

    public void final_state()
    {
        game_over.gameObject.SetActive(true);
    }
}
