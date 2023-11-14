using UnityEngine;
public class FollowZombieController : ZombieController
{
    public Transform Taget;


    override protected InputInfo GetInput()
    {
        InputInfo input = new InputInfo();

        Vector3 tagetPosition = transform.InverseTransformPoint(Taget.transform.position);

        if(tagetPosition.z > 0)
            input.forward = true;
        if (tagetPosition.x > 0)
            input.right = true;
        if (tagetPosition.x < 0)
            input.left = true;
        if ((Taget.transform.position - transform.position).magnitude > 10)
            input.run = true;

        return input;
    }
}
