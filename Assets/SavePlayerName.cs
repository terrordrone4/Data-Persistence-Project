using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerName : MonoBehaviour
{
    public void clearInputField(TMP_InputField field)
    {
        field.text = string.Empty;
    }
    public void startGame(TMP_InputField plyrName_field)
    {
        DataPersitance.Instance.PlayerEnterWith(plyrName_field.text);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
