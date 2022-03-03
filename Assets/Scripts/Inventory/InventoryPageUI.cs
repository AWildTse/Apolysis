using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPageUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private Transform _inventorySlotParent;
    private GameObject _newInventorySlot;

    #region Inventory Slot Information
    private int _resetXPos = 200;
    private int _xPos = 200;
    private const int _incrementAmount = 75;
    private int _resetYPos = 250;
    private int _yPos = 250;
    private const int _zPos = 0;
    private Vector3 _position;
    private const int _startScale = 1;
    private const int _width = 70;
    private const int _height = 70;
    #endregion

    #region Page Information
    private const int _rows = 6;
    private const int _columns = 5;
    #endregion

    private void Start()
    {       
        CreateInventoryPage();
    }

    public void CreateInventoryPage()
    {
        for (int row = 0; row < _rows; row++)
        {
            for(int col = 0; col < _columns; col++)
            {
                _inventorySlotPrefab = Instantiate(_inventorySlotPrefab, _position, Quaternion.identity, _inventorySlotParent);
                _position = new Vector3(_xPos, _yPos, _zPos);
                _inventorySlotPrefab.transform.localPosition = _position;
                _xPos = _xPos + _incrementAmount;
            }
            _xPos = _xPos - (_columns * _incrementAmount);
            _yPos = _yPos - _incrementAmount;
        }
        _yPos = _yPos - (_rows * _incrementAmount);
    }
}
