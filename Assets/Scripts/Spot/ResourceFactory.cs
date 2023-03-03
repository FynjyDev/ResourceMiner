using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceFactory : Spot
{
    [Header("Resource Factory Pharameters")]

    public ResourceController.ResourceTypes takeResourceType;
    public Slider resourceFactoryProgressBar;
    public TextMeshProUGUI takeResourcesProgressCount;
    public MovableResource spotTakeResourcePrefab;

    public Transform takeResourceTarget;

    private bool _IsTakeResources;
    private bool _IsAllResourcesTaken;
    private int _TempTakeResourcesCount;

    private Vector3 _LastCharacterPos;
    private float _TakeResourcesDelay => Settings.s.takeResourcesSpawnDelay;
    private float _TakeResourcesMoveTime => Settings.s.takeResourcesMoveTime;
    private float _DelayBeforeGiveResources => Settings.s.delayBeforeGiveResources;
    private float _GiveResourceDelay => Settings.s.giveResourcesDelay;
    private int _MaxTakeResourcesCount => Settings.s.takeResourcesCount;
    private int _GiveResourcesCount => Settings.s.giveResourcesCount;

    private void Awake()
    {
        resourceFactoryProgressBar.maxValue = _DelayBeforeGiveResources;
        resourceFactoryProgressBar.value = 0;
        resourceFactoryProgressBar.gameObject.SetActive(false);
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        if (!characterCollision.characterMovement.joystick.isMove && !_IsTakeResources)
        {
            StartCoroutine(TakeResources());
            _IsTakeResources = true;
        }
    }

    public override void OnPlayerStay()
    {
        base.OnPlayerStay();

        if (characterCollision.characterMovement.joystick.isMove)
        {
            if (!_IsAllResourcesTaken) StopAllCoroutines();
            _LastCharacterPos = characterCollision.transform.position;
            _IsTakeResources = false;
        }
        else if (!_IsTakeResources)
        {
            StartCoroutine(TakeResources());
            _IsTakeResources = true;
        }
    }

    public override void OnPlayerExit()
    {
        if (!_IsAllResourcesTaken) StopAllCoroutines();
        characterCollision = null;
        base.OnPlayerExit();
    }

    public IEnumerator TakeResources()
    {
        while (_TempTakeResourcesCount < _MaxTakeResourcesCount)
        {
            MovableResource _newMovableResource = Instantiate(spotTakeResourcePrefab, _LastCharacterPos, Quaternion.identity);

            _newMovableResource.moveTime = _TakeResourcesMoveTime;
            _newMovableResource.target = takeResourceTarget;

            _newMovableResource.StartCoroutine(_newMovableResource.Move(this));
            _TempTakeResourcesCount++;

            yield return new WaitForSeconds(_TakeResourcesDelay);
        }
    }

    public void OnResourceTaken()
    {
        resourceController.OnResourceValueChange(takeResourceType, false);
        takeResourcesProgressCount.text = $"{_TempTakeResourcesCount} / {_MaxTakeResourcesCount}";

        if (_TempTakeResourcesCount >= _MaxTakeResourcesCount)
        {
            StartCoroutine(DelayBeforeGiveResources());
            _IsAllResourcesTaken = true;
        }
    }

    public IEnumerator DelayBeforeGiveResources()
    {
        resourceFactoryProgressBar.gameObject.SetActive(true);

        float _time = 0;
        while (_time <= _DelayBeforeGiveResources)
        {
            _time += Time.deltaTime;
            resourceFactoryProgressBar.value = _time;

            yield return null;
        }

        resourceFactoryProgressBar.gameObject.SetActive(false);
        StartCoroutine(GiveResources());

    }

    public IEnumerator GiveResources()
    {
        int _tempResources = 0;

        while (_tempResources < _GiveResourcesCount)
        {
            SpawnResource();
            _tempResources++;
            yield return new WaitForSeconds(_GiveResourceDelay);
        }

        _IsTakeResources = false;
        _IsAllResourcesTaken = false;
        _TempTakeResourcesCount = 0;
        takeResourcesProgressCount.text = $"{_TempTakeResourcesCount} / {_MaxTakeResourcesCount}";
    }

}
