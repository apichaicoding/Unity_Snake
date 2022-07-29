using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public BoxCollider2D Zone;

    private void Start(){
        RandomizePosition();
    }

    private void RandomizePosition(){
        //Drop Random in Zone
        Bounds bounds = this.Zone.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            RandomizePosition();
            Time.fixedDeltaTime -= 0.0005f;
        }
    }
}
