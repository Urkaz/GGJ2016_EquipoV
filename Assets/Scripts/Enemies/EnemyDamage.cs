using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

    public int hits = 2;
    private Material dissolveMaterial;
    private bool destroying = false;

    private float shaderSliceAmount = 0.15f;

    public virtual void Damage(int amount) {
        if (hits - amount <= 0)
            Destroy();
        else
            hits -= amount;
    }

    public void Destroy() {
        //Texture2D mainTexture = Resources.Load("????") as Texture2D;
        Texture2D noiseTexture = Resources.Load("Images/noise") as Texture2D;
        Texture2D burnRampTexture = Resources.Load("Images/burn") as Texture2D;

        /*dissolveMaterial = new Material(Shader.Find("Custom/Dissolve"));
        //dissolveMaterial.SetTexture("_MainTex",mainTexture);
        dissolveMaterial.SetTexture("_SliceGuide", noiseTexture);
        dissolveMaterial.SetTexture("_BurnRamp", burnRampTexture);
        dissolveMaterial.SetFloat("_SliceAmount", 0.15f);

        GetComponent<Renderer>().material = dissolveMaterial;*/
        destroying = true;
    }

    public void Update() {
        if (destroying) {
            if (shaderSliceAmount + Time.deltaTime < 0.75f)
                shaderSliceAmount += Time.deltaTime;
            else {
                shaderSliceAmount = 0.75f;
                Destroy(gameObject);
            } 

            dissolveMaterial.SetFloat("_SliceAmount", shaderSliceAmount);
        }
    }
}
