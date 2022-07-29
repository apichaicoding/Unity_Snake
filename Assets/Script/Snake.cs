using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();

    public Transform segmentsPrefab;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public int gameStartScene;

    float timecount = 0;
    int score = 0;
    
    private void Start(){
        ResetState();
    }

    private void Update()
    {
        // Move Snake
        if(Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down ){
            _direction = Vector2.up;
        }else if(Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up){
            _direction = Vector2.down;
        }else if(Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right){
            _direction = Vector2.left;
        }else if(Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left){
            _direction = Vector2.right;
        }
        //Count Time
        timecount += Time.deltaTime;
        DisplayTime(timecount);
    }

    private void FixedUpdate()
    {
        //Grow Follow
        for(int i = _segments.Count -1; i > 0; i-=1){
            _segments[i].position = _segments[i-1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow(){
        //Grow Add
        Transform segment = Instantiate(this.segmentsPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        //Add Score
        score += 1 ;
        scoreText.text = score.ToString() + " : Score" ;
    }

    void DisplayTime(float timeToDisplay)
    {
        //Format Time
        TimeSpan ts = TimeSpan.FromSeconds(timeToDisplay);
        timeText.text = "Time : " + ts.ToString("hh\\:mm\\:ss");
    }

    private void ResetState(){
        // Clear Grow 
        for(int i = 1; i < _segments.Count; i+=1){
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        // Grow defaut
        for(int i = 0; i < 4; i+=1){
            _segments.Add(Instantiate(this.segmentsPrefab));
        }
        this.transform.position = Vector3.zero;
        score = 0;
        scoreText.text = score.ToString()+" : Score";
        //Speed
        Time.fixedDeltaTime = 0.12f;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Food"){
            Grow();
        }else if(other.tag == "ObjectDead"){
            Result.resulttime = timeText;
            Result.resultscore = scoreText;
            SceneManager.LoadScene(gameStartScene);
        }
    }
}
