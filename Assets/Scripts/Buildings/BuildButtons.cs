using UnityEngine;

public class BuildButtons : MonoBehaviour
{
    public bool levelUp;
    public bool assign;
    public bool close;

    public Build_Object thisBuild;

    private void OnMouseDown()
    {
        if (!assign && !levelUp && !close)
            return;
        if (levelUp)
        {
            thisBuild.LevelUpBuild();
            transform.parent.gameObject.SetActive(false);
            return;
        }
        if (assign)
        {
            GameManager.instance.SetDigimonList(true);
            GameManager.instance.SetBuildToAssignDigimon(thisBuild);
            transform.parent.gameObject.SetActive(false);
            return;
        }
        if (close)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
    }
}