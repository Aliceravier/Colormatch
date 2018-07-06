using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public static class InputHelper {

    //TODO: move outta here
    /* Hacky method to get whether control scheme is a controller or keyboard */
    public static bool isControllerInput(PlayerID playerInputID)
    {
        return InputManager.GetControlScheme(playerInputID).GetAction(0).Bindings[0].Type == InputType.AnalogAxis;
    }
}
