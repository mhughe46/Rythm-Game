using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathData : MonoBehaviour
{
    [SerializeField]
    List<Transform> _pathsEmpty;

    [SerializeField]
    List<Transform> _pathsJumpObis;

    [SerializeField]
    List<Transform> _pathsSlideObis;

    [SerializeField]
    List<Transform> _pathsDodgeRight;

    [SerializeField]
    List<Transform> _pathsDodgeLeft;

    public Dictionary<string, Path> pathsLibrary = new Dictionary<string, Path>();

    private void Start()
    {
        PopulatePathsLibrary();
        
    }

    private void PopulatePathsLibrary()
    {
        ReadPathList(_pathsEmpty, PathType.Empty);
        ReadPathList(_pathsJumpObis, PathType.Jump);
        ReadPathList(_pathsSlideObis, PathType.Slide);
        ReadPathList(_pathsDodgeRight, PathType.Right);
        ReadPathList(_pathsDodgeLeft, PathType.Left);
    }

    private List<Path> ReadPathList(List<Transform> pathList, PathType pathType)
    {
        List<Path> pathInfo = new List<Path>();
        foreach (Transform path in pathList)
        {
            Path newPath = new Path();
            newPath.id = path.GetComponent<RotatePath>()._pathId;
            newPath.difficulty = path.GetComponent<RotatePath>()._pathDifficulty;
            newPath.theme = path.GetComponent<RotatePath>()._pathTheme;
            newPath.pathType = pathType;
            newPath.pathObject = path;

            if (!pathsLibrary.ContainsKey(newPath.id))
            {
                pathsLibrary.Add(newPath.id, newPath);
            }
        }
        return pathInfo;
    }
}

public struct Path
{
    public string id;
    public Transform pathObject;
    public int difficulty;
    public LevelTheme theme;
    public PathType pathType;
}

public enum PathType
{
    Empty,
    Jump,
    Slide,
    Right,
    Left
}
