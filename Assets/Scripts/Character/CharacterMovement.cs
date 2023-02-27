using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public Animator characterAnimator;
    public Transform characterTr;
    public NavMeshAgent characterNavMesh;
    public Joystick joystick;

    [SerializeField] private float _MoveSpeed;
    [SerializeField] private float _RotateSpeed;

    private Vector3 _InputVector;

    private void FixedUpdate()
    {
        CharacterMove();
        CharacterRotate();
    }

    private void CharacterMove()
    {
        _InputVector.x = joystick.Horizontal;
        _InputVector.z = joystick.Vertical;

        characterNavMesh.Move(_InputVector * _MoveSpeed);
        characterAnimator.SetBool("IsRun", _InputVector != Vector3.zero);
    }

    private void CharacterRotate()
    {
        if (_InputVector == Vector3.zero) return;

        characterTr.rotation = Quaternion.Lerp(characterTr.rotation,
            Quaternion.LookRotation(new Vector3(_InputVector.x, 0, _InputVector.z)), _RotateSpeed);
    }
}
