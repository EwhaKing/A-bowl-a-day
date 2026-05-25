// Assets/Scripts/Interactable/Ingredient.cs
using UnityEngine;

public class Ingredient : DraggableObject
{
    public enum IngredientType { Sauce, Tteok, Eomuk, Pa, Yangbaechu }
    public IngredientType type;

    [SerializeField] private float snapDistance = 1.5f;
    private Transform _potTransform;

    protected override void Start()
    {
        base.Start();
        _potTransform = GameObject.FindWithTag("Pot").transform;
    }

    protected override void OnReleased()
    {
        float dist = Vector3.Distance(transform.position, _potTransform.position);
        GameState state = GameManager.Instance.CurrentState;

        // 물이 들어간 이후 조건 검사
        bool canDrop = (state == GameState.WaterAdded || state == GameState.IngredientsAdding);

        if (dist < snapDistance && canDrop)
        {
            StartCoroutine(DropRoutine());
        }
        else
        {
            ReturnToStart();
        }
    }

    System.Collections.IEnumerator DropRoutine()
    {
        CanDrag = false;
        yield return MoveBack(_potTransform.position);

        // 스케일 줄어들며 퐁당 빠지는 연출
        float t = 0f;
        Vector3 originScale = transform.localScale;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originScale, Vector3.zero, t / 0.2f);
            yield return null;
        }

        CheckRegistration();
        gameObject.SetActive(false); // 냄비 속으로 쏙 사라짐
    }

    void CheckRegistration()
    {
        var gm = GameManager.Instance;
        if (type == IngredientType.Sauce) gm.SauceAdded = true;
        if (type == IngredientType.Tteok) gm.TteokAdded = true;
        if (type == IngredientType.Eomuk) gm.EomukAdded = true;
        if (type == IngredientType.Pa) gm.PaAdded = true;
        if (type == IngredientType.Yangbaechu) gm.YangbaechuAdded = true;

        gm.SetState(GameState.IngredientsAdding);
        UIManager.Instance.UpdateIngredientStatus();

        // 5가지 재료가 완전히 다 들어가면 강불 조리 상태 진입!
        if (gm.AllIngredientsIn())
        {
            gm.SetState(GameState.Cooking_HighFlame);
            Pot.Instance.SetCookingVisual();
        }
    }
}