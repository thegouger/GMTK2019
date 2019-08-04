using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [SerializeField] private float fullCharge = 25f;
    [SerializeField] private int id = 0;
    private float chargeAmount = 0f;

    private Sprite activeSprite;
    private SpriteRenderer spriteRenderer;
    private string assetPath = "Generators/";

    private Sprite g0;
    private Sprite g1;
    private Sprite g2;
    private Sprite g3;
    private Sprite g4;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        g0 = Resources.Load<Sprite>(assetPath + "generator0");
        g1 = Resources.Load<Sprite>(assetPath + "generator1");
        g2 = Resources.Load<Sprite>(assetPath + "generator2");
        g3 = Resources.Load<Sprite>(assetPath + "generator3");
        g4 = Resources.Load<Sprite>(assetPath + "generator4");

        bool completed = GlobalState.generators[id];
        if (completed) {
            chargeAmount = fullCharge;
            activeSprite = g4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeSprite != null && !activeSprite.Equals(spriteRenderer.sprite)) {
            spriteRenderer.sprite = activeSprite;
        }
    }

    public void Charge(float chargeAmountDelta) {
        if (chargeAmount >= fullCharge) {
            return;
        }
        chargeAmount += chargeAmountDelta;
        if (chargeAmount < 0.1) {
            activeSprite = g0;
        } else if (chargeAmount < 8) {
            activeSprite = g1;
        } else if (chargeAmount < 16) {
            activeSprite = g2;
        } else if (chargeAmount >= fullCharge){
            activeSprite = g3;
            GlobalState.generators[id] = true;
            StartCoroutine(UpdateGenerator());
        }
    }

    private IEnumerator UpdateGenerator() {
        yield return new WaitForSeconds(0.5f);
        activeSprite = g4;
    }
}
