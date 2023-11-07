using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    MeshRenderer rend;
    Color startColor;
    bool full;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (CompareTag("EnemyNode"))
            return;
        if(BuildManager.Instance.GetBuildInfo() == null || full)
        {
            return;
        }
        GameObject newBuild = Instantiate(BuildManager.Instance.GetPrefab(), new Vector3(transform.position.x, transform.position.y + 1.7f, transform.position.z), Quaternion.identity);
        newBuild.tag = "BuildAlly";
        newBuild.transform.Rotate(new Vector3(60, 0, 0));
        Build_Object props = newBuild.GetComponent<Build_Object>();
        props.SetBuild(BuildManager.Instance.GetBuildInfo());
        full = true;
        BuildManager.Instance.SetBuildInfo(null);
    }

    public void SetFull(bool state)
    {
        full = state;
    }

    public bool GetFull()
    {
        return full;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}