using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathParserTK;

public class Graph : MonoBehaviour {

    public Transform pointPrefab;
    [Range(10, 100)]
    public int resolution = 10;
    public bool animate = true;

    private Transform[] mPoints = new Transform[]{};
    private MathParser mMathParser = new MathParser('.');
    private string mStringFunction;
    private bool mShowGraph = false;

    private void Awake(){
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        scale.z = 3f;
        Vector3 position = Vector3.zero;

        mPoints = new Transform[resolution];
        for (int i = 0; i < resolution; i++)
        {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);

            mPoints[i] = point;
        }

        SetGraphVisibility(mShowGraph);
    }

    private void Update(){
        if (!mShowGraph) return;

        for (int i = 0; i < mPoints.Length; i++)
        {
            Transform point = mPoints[i];
            Vector3 position = point.localPosition;

            var fClone = mStringFunction;
            fClone = fClone.Replace("x", "("+position.x.ToString()+")").Replace("t", (animate ? Time.time.ToString() : "0"));
            double functionResult = 0;

            if(!mMathParser.TryParse(fClone, out functionResult, true)){
                UIManager.instance.SetMessageInInputField("Invalid Function");
                continue;
            }

            position.y = (float)functionResult;
            point.localPosition = position;
        }
    }

    public void SetStringFunction(string function){
        mStringFunction = function;
        mShowGraph = true;
        SetGraphVisibility(mShowGraph);
    }

    private void SetGraphVisibility(bool show){
        for (int i = 0; i < mPoints.Length; i++)
        {
            mPoints[i].gameObject.SetActive(show);
        }
    }
}
