using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatePlayer : MonoBehaviour
{
    public InputField playerName;
    public Image playerAnimal;

    public Sprite[] animals;
    private int currentAnimal = 0;

    public void NextAnimal()
    {
        Debug.Log("Next");
        if(currentAnimal == animals.Length)
        {
            currentAnimal = 0;
        }
        else
        {
            currentAnimal += 1;
        }
        playerAnimal.sprite = animals[currentAnimal];
    }

    public void PrevAnimal()
    {
        Debug.Log("Prev");
        if(currentAnimal == 0)
        {
            currentAnimal = animals.Length;
        }
        else
        {
            currentAnimal -= 1;
        }
        playerAnimal.sprite = animals[currentAnimal];
    }

    public void ConfirmName()
    {
        Debug.Log("Name: " + playerName.text);
        PlayerData.playerName = playerName.text;

    }

    public void ConfirmAnimal()
    {
        Debug.Log("Animal: " + playerAnimal.name);
        PlayerData.playerAnimal = playerAnimal.sprite;
        SceneManager.LoadScene("NewCarController");
    }


}
