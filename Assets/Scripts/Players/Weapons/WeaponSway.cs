using UnityEngine;
using Sirenix.OdinInspector;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] public Transform weaponTransform;
    public GunItem currentGun;

    [BoxGroup("Sway Properties")]
    [SerializeField]private float aimSwayAmount = 0.01f;
    [BoxGroup("Sway Properties")]
    [SerializeField]private float swayAmount = 0.01f;
    [BoxGroup("Sway Properties")]
    [SerializeField]private float currentSwayAmount = 0.01f;
    [BoxGroup("Sway Properties")]
    [SerializeField] public float maxSwayAmount = 0.1f;
    [BoxGroup("Sway Properties")]
    [SerializeField] public float swaySmooth = 9f;
    [BoxGroup("Sway Properties")]
    [SerializeField] public AnimationCurve swayCurve;

    [BoxGroup("Sway Properties")]
    [Range(0f, 1f)]
    [SerializeField] public float swaySmoothCounteraction = 1f;

    [BoxGroup("Rotation")]
    [SerializeField] public float rotationSwayMultiplier = 1f;

    [BoxGroup("Position")]
    [SerializeField] public float positionSwayMultiplier = -1f;
    

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector2 sway;
    public bool aiming;
    private void Reset()
    {
        Keyframe[] ks = new Keyframe[] { new Keyframe(0, 0, 0, 2), new Keyframe(1, 1) };
        swayCurve = new AnimationCurve(ks);
    }

    private void Start()
    {
        if (!weaponTransform)
            weaponTransform = transform;
        initialPosition = weaponTransform.localPosition;
        initialRotation = weaponTransform.localRotation;
        currentSwayAmount = swayAmount;
    }

    private void Update()
    {
        if(aiming){
            currentSwayAmount = aimSwayAmount;
        }else{
            currentSwayAmount = swayAmount;
        }
        if(currentGun){

            float mouseX = Input.GetAxis("Mouse X") * currentSwayAmount * currentGun.weight;
            float mouseY = Input.GetAxis("Mouse Y") * currentSwayAmount * currentGun.weight;

            sway = Vector2.MoveTowards(sway, Vector2.zero, swayCurve.Evaluate(Time.deltaTime * swaySmoothCounteraction * sway.magnitude * swaySmooth));
            sway = Vector2.ClampMagnitude(new Vector2(mouseX, mouseY) + sway, maxSwayAmount);

            weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, new Vector3(sway.x, sway.y, 0) * positionSwayMultiplier + initialPosition, swayCurve.Evaluate(Time.deltaTime * swaySmooth));
            weaponTransform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation * Quaternion.Euler(Mathf.Rad2Deg * rotationSwayMultiplier * new Vector3(-sway.y, sway.x, 0)), swayCurve.Evaluate(Time.deltaTime * swaySmooth));
        }else{
            Debug.Log("No Gun");
        }
    }
}