[System.Serializable]
public class Coordinate {
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class Pattern {
    public Coordinate[] coordinates;
}

[System.Serializable]
public class Patterns {
    public Pattern[] patterns;
}