using UnityEngine;

public class KeyZombieController : ZombieController
{
    public string ForwardKey = "w";
    public string RunKey = "left shift";
    public string LeftKey = "a";
    public string RightKey = "d";
    public string JumpKey = "space";

    override protected InputInfo GetInput()
    {
        InputInfo input = new InputInfo();
        input.forward = Input.GetKey(ForwardKey.ToLower());
        input.run = Input.GetKey(RunKey.ToLower());
        input.left = Input.GetKey(LeftKey.ToLower());
        input.right = Input.GetKey(RightKey.ToLower());
        input.jump = Input.GetKey(JumpKey.ToLower());
        return input;
    }
    
}
