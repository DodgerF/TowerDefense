using System.Collections.Generic;
using System.Linq;
using TMPro;
using TowerDefense;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _leftButton;

    [SerializeField] private List<UpgradeAsset> _upgradeAssets = new List<UpgradeAsset>();
    [SerializeField] private ShopCell _cellPref;

    [SerializeField] private TextMeshProUGUI _playerMoney;
    private int _money;
    public int PlayerMoney => _money;
    private List<ShopCell> _cells = new List<ShopCell>();
    private int _index;
    private const int _cellsOnScreen = 3;

    private void Awake()
    {
        _index = _cellsOnScreen - 1;

        UpdateMoney();

        InitializeCells();
        InitializeButtons();
    }
    public void UpdateMoney()
    {
        if (MapCompletion.Instance)
        {
            _money = MapCompletion.Instance.TotalScore;
            _money -= Upgrades.GetSpentScore();
            _playerMoney.text = _money.ToString();
        }
        else
        {
            Debug.LogWarning("MapCompletion does not exist");
        }
        foreach (ShopCell cell in _cells) 
        {
            cell.CheckCost();
        }
    }
    private void OnDisable()
    {
        _leftButton.onClick.RemoveAllListeners();
        _rightButton.onClick.RemoveAllListeners();
    }
    private void InitializeCells()
    {
        _cells = GetComponentsInChildren<ShopCell>().ToList();
        if (_cells.Count < _cellsOnScreen)
        {
            Debug.LogWarning($"The shop must initially have {_cellsOnScreen} cells");
        }

        for (int i = 0; i < _upgradeAssets.Count; i++)
        {
            if (i >= _cells.Count)
            {
                var cell = Instantiate(_cellPref);
                cell.transform.SetParent(transform);
                cell.gameObject.SetActive(false);
                _cells.Add(cell);
            }
            _cells[i].Initialize(_upgradeAssets[i]);
        }
    } 

    private void InitializeButtons()
    {
        _rightButton.onClick.AddListener(OnRightClick);
        _leftButton.onClick.AddListener(OnLeftClick);
        SetButtonsActive();
    }

    private void OnRightClick()
    {
        _cells[_index - _cellsOnScreen + 1].gameObject.SetActive(false);
        _index++;
        _cells[_index].gameObject.SetActive(true);

        for (int i = 0; i < _cellsOnScreen; i++)
        {
            _cells[_index - i].transform.position = _cells[_index - 1 - i].transform.position;

        }
        SetButtonsActive();
    }
    private void OnLeftClick()
    {
        _cells[_index].gameObject.SetActive(false);
        _index--;
        _cells[_index - _cellsOnScreen + 1].gameObject.SetActive(true);
        for (int i = 0; i < _cellsOnScreen; i++)
        {
            _cells[_index - _cellsOnScreen + 1 + i].transform.position = _cells[_index - _cellsOnScreen + 1 + 1 + i].transform.position;
        }
        SetButtonsActive();
    }
    private void SetButtonsActive()
    {
        _leftButton.interactable = _cellsOnScreen < _index + 1;
        _rightButton.interactable = _index + 1 < _cells.Count;
    }
}
