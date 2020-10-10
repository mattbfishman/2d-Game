using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health_Script : MonoBehaviour
{
    private GameObject player;
    private GameObject healthCanvas;
    private Sprite[] sprites;
    public string ResourcePath;
    private float currentHealth; 
    private float startingHealth;
    private float newHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthCanvas = GameObject.FindGameObjectWithTag("HealthCanvas");
        startingHealth = player.GetComponent<Platformer>().health;

        currentHealth = startingHealth;

        setStartingHealth(startingHealth);

    }

    // Update is called once per frame
    void Update()
    {
        newHealth = player.GetComponent<Platformer>().health;
        if(newHealth != currentHealth){
            setCurrentHealth(newHealth);
            newHealth = currentHealth;
        }
        
    }

    void setStartingHealth(float health){
        if(health > 0){
            sprites = Resources.LoadAll<Sprite>(ResourcePath);
            Sprite healthImg = getSpriteByName("Heart_full");
            for(float i = 0; i < health; i++){
                GameObject newObj = new GameObject("Health");
                Image newImage = newObj.AddComponent<Image>(); //Add the Image Component script
                newImage.rectTransform.sizeDelta = new Vector2(16, 16);
                newImage.rectTransform.localPosition = new Vector3((16 * (i + 1)) + (i * 5), 425, 0);
                newImage.sprite = healthImg; //Set the Sprite of the Image Component on the new GameObject
                newObj.GetComponent<RectTransform>().SetParent(healthCanvas.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            }
        }
    }

    void setCurrentHealth(float health){
        if(health < currentHealth && health >= 0){
            for(var i = healthCanvas.transform.childCount-1; i >= health; i--){
                var currentHeart = healthCanvas.transform.GetChild(i);
                currentHeart.gameObject.SetActive(false);
            }
        }
    }

    public Sprite getSpriteByName(string name){
        foreach(var sprite in sprites){
            if(sprite.name == name){
                return sprite;
            }
        }
        return null;
    }
}
