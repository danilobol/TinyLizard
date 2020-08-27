using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIBehaviour : MonoBehaviour
{

    [HideInInspector()]
    public CharacterStatus characterStatus;
    public Text forceText;

    [Header("")]
    public PlayerStatusUI playerStatusUI;

    public void SetOwnerPlayer(CharacterStatus characterStatus,int force)
    {
        if (characterStatus == null)
        {
            return;
        }

        playerStatusUI.SetCharacterBehaviour(characterStatus);

        this.characterStatus = characterStatus;
        forceText.text = force.ToString();

 
    }

}
