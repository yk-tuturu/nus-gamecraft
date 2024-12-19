using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;
    List<Vector3> pat1 = new List<Vector3>() {
        new Vector3(-1.35f,0.45f,0),
        new Vector3(-0.2f,-0.64f,0),
        new Vector3(0.9f,-0.43f,0)
    };

    List<Vector3> pat2 = new List<Vector3>() {
        new Vector3(-1.35f,-0.44f,0),
        new Vector3(-0.2f,0.64f,0),
        new Vector3(0.9f,-0.42f,0)
    };

    List<Vector3> pat3 = new List<Vector3>() {
        new Vector3(-0.36f,0.89f,0),
        new Vector3(-1.29f,-0.01f,0),
        new Vector3(0.41f,-1.03f,0)
    };

    List<Vector3> pat4 = new List<Vector3>() {
        new Vector3(0.12f,1.02f,0),
        new Vector3(1.22f,0.38f,0),
        new Vector3(-0.1f,-1.06f,0)
    };

    List<Vector3> pat5 = new List<Vector3>() {
        new Vector3(-1.11f,0.74f,0),
        new Vector3(-0.21f,-0.16f,0),
        new Vector3(0.73f,-1.07f,0)
    };

    List<Vector3> pat6 = new List<Vector3>() {
        new Vector3(0.83f,0.74f,0),
        new Vector3(-0.04f,-0.1f,0),
        new Vector3(-0.96f,-0.96f,0)
    };


    public List<List<Vector3>> patternList = new List<List<Vector3>>();

    public List<float> beatTimings = new List<float>() {
0.0f, 0.594f, 1.188f, 1.782f, 2.376f, 2.97f, 3.564f, 3.861f, 4.158f, 4.752f, 5.346f, 5.94f, 6.534f, 7.128f, 7.722f, 8.316f, 8.91f, 9.207f, 9.504f, 10.099f, 10.693f, 11.287f, 11.881f, 12.475f, 13.069f, 13.366f, 13.663f, 14.257f, 14.851f, 15.445f, 16.039f, 16.336f, 16.633f, 17.227f, 17.821f, 18.415f, 18.712f, 19.009f, 19.603f, 20.198f, 20.792f, 21.386f, 21.98f, 22.574f, 22.871f, 23.168f, 23.762f, 24.059f, 24.356f, 24.95f, 25.544f, 26.138f, 26.732f, 27.326f, 27.92f, 28.217f, 28.514f, 29.108f, 29.702f, 30.297f, 30.891f, 31.485f, 32.079f, 32.376f, 32.673f, 33.267f, 33.564f, 33.861f, 34.455f, 35.049f, 35.643f, 36.237f, 36.831f, 37.425f, 37.722f, 38.019f, 38.613f, 39.207f, 39.801f, 40.396f, 40.99f, 41.287f, 41.881f, 42.178f, 42.475f, 42.772f, 43.366f, 43.96f, 44.554f, 45.148f, 45.742f, 46.336f, 46.93f, 47.227f, 47.524f, 48.118f, 48.712f, 49.306f, 49.9f, 50.495f, 50.792f, 51.386f, 51.683f, 51.98f, 52.277f, 52.871f, 53.465f, 54.059f, 54.356f, 54.653f, 55.247f, 55.841f, 56.435f, 56.732f, 57.029f, 57.623f, 58.217f, 58.811f, 59.405f, 60.0f, 60.297f, 60.594f, 60.891f, 61.188f, 61.782f, 62.079f, 62.376f, 62.97f, 63.564f, 64.158f, 64.455f, 64.752f, 65.346f, 65.94f, 66.534f, 67.128f, 67.425f, 67.722f, 68.316f, 68.91f, 69.504f, 69.801f, 70.099f, 70.396f, 70.693f, 71.287f, 71.584f, 71.881f, 72.475f, 73.069f, 73.663f, 73.96f, 74.257f, 74.851f, 75.445f, 76.039f, 76.633f, 77.227f, 77.821f, 78.415f, 79.009f, 79.306f, 79.9f, 80.198f, 80.495f, 80.792f, 81.386f, 81.98f, 82.574f, 83.168f, 83.762f, 84.356f, 84.95f, 85.247f, 85.544f, 86.138f, 86.732f, 87.326f, 87.92f, 88.514f, 89.108f, 89.405f, 89.702f, 90.297f, 90.891f, 91.485f, 92.079f, 92.673f, 93.267f, 93.861f, 94.455f, 94.752f, 95.049f, 95.643f, 96.237f, 96.831f, 97.425f, 98.019f, 98.613f, 98.91f, 99.207f, 99.801f, 100.396f, 100.99f, 101.584f, 102.178f, 102.772f, 103.366f, 103.96f, 104.257f, 104.554f, 105.148f, 105.742f, 106.336f, 106.93f, 107.524f, 107.821f, 108.118f, 108.415f, 108.712f, 109.306f, 109.603f, 109.9f, 110.495f, 111.089f, 111.683f, 111.98f, 112.277f, 112.574f, 112.871f, 113.465f, 114.059f, 114.653f, 115.247f, 115.841f, 116.435f, 117.029f, 117.326f, 117.623f, 117.92f, 118.217f, 118.811f, 119.108f, 119.405f, 119.999f, 120.594f, 121.188f, 121.485f, 121.782f, 122.376f, 122.97f, 123.564f, 124.455f, 125.049f, 125.94f, 126.831f, 127.425f, 128.316f, 129.207f, 129.801f, 130.693f, 131.881f, 132.475f, 133.069f, 133.96f, 134.554f, 135.445f, 136.336f, 136.93f, 137.821f, 138.712f, 139.306f, 140.198f, 141.386f, 142.574f, 143.465f, 144.059f, 144.95f, 145.841f, 146.435f, 147.326f, 148.217f, 148.811f, 149.702f, 150.891f, 152.079f, 152.97f, 153.564f, 154.455f, 155.346f, 155.94f, 156.831f, 157.722f, 158.316f, 159.207f, 160.396f, 161.584f, 162.178f, 162.772f, 163.366f, 163.96f, 164.554f, 165.148f, 165.445f, 165.742f, 166.336f, 166.93f, 167.524f, 168.118f, 168.712f, 169.306f, 169.9f, 170.495f, 170.792f, 171.089f, 171.683f, 172.277f, 172.871f, 173.465f, 174.059f, 174.653f, 174.95f, 175.247f, 175.841f, 176.435f, 177.029f, 177.623f, 178.217f, 178.811f, 179.405f, 180.0f, 180.297f, 180.594f, 181.188f, 181.782f, 182.376f, 182.97f, 183.564f, 183.861f, 184.158f, 184.455f, 184.752f, 185.346f, 185.643f, 185.94f, 186.534f, 187.128f, 187.722f, 188.019f, 188.316f, 188.91f, 189.504f, 190.099f, 190.693f, 191.287f, 191.881f, 192.475f, 193.069f, 193.366f, 193.663f, 193.96f, 194.257f, 194.851f, 195.148f, 195.445f, 196.039f, 196.633f, 197.227f, 197.524f, 197.821f, 198.415f, 199.009f, 199.603f, 200.198f, 200.792f, 201.386f, 201.98f, 202.574f, 202.871f, 203.168f, 203.465f, 203.762f, 204.356f, 204.653f, 204.95f, 205.247f, 205.544f, 206.138f, 206.732f, 207.029f, 207.326f, 207.92f, 208.514f, 209.108f, 209.702f, 210.297f, 210.891f, 211.485f, 212.079f, 212.376f, 212.673f, 212.97f, 213.267f, 213.861f, 214.158f, 214.455f, 214.752f, 215.049f, 215.643f, 216.237f, 216.534f, 216.831f, 217.425f, 218.019f, 218.613f, 219.207f, 219.801f, 220.396f, 220.99f, 221.584f, 222.178f, 222.475f, 222.772f, 223.366f, 223.663f, 223.96f, 224.554f, 225.148f, 225.742f, 226.336f, 226.93f, 227.524f, 227.821f, 228.118f, 228.712f, 229.306f, 229.9f, 230.495f, 231.089f, 231.683f, 231.98f, 232.277f, 232.871f, 233.465f, 234.059f, 234.653f, 235.247f, 235.841f, 236.435f, 237.029f, 237.326f, 237.623f, 238.217f, 238.811f, 239.405f, 240.0f, 240.594f, 241.188f, 241.485f, 241.782f, 242.376f, 242.97f, 243.564f, 244.158f, 244.752f, 245.346f, 245.94f, 246.534f, 246.831f, 247.128f, 247.722f, 248.316f, 248.91f, 249.504f, 250.099f, 250.693f, 250.99f, 251.287f, 251.881f, 252.475f, 253.069f, 253.663f, 254.257f, 254.851f, 255.742f, 256.93f, 259.009f
    };

    public AudioSource audio;
    public AudioClip hitSound;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        patternList.Add(pat1);
        patternList.Add(pat2);
        patternList.Add(pat3);
        patternList.Add(pat4);
        patternList.Add(pat5);
        patternList.Add(pat6);
    }

    void Update() {
        float songPos = MusicManager.instance.GetSongPos();
        if (songPos > beatTimings[0]) {
            float temp = beatTimings[0];
            beatTimings.RemoveAt(0);
            beatTimings.Add(temp + MusicManager.instance.songLength);

            //audio.PlayOneShot(hitSound);
        }

        //DEBUG
    }

    public List<Vector3> getPattern() {
        int index = Random.Range(0, patternList.Count);

        List<Vector3> newList = new List<Vector3>();
        for (int i = 0; i < patternList[index].Count; i++) {
            newList.Add(new Vector3(patternList[index][i].x, patternList[index][i].y, patternList[index][i].z));
        }
        return newList;
    }

    public List<float> getBeats(float start, int count) {
        int i = 0;
        int counter = 0;

        List<float> beats = new List<float>();
        while (counter < count) {
            if (beatTimings[i] > start) {
                beats.Add(beatTimings[i]);
                counter++;
            }
            i++;
        }

        return beats;
    } 
}
