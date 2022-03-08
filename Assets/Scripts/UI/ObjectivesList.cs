using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectivesList : MonoBehaviour
{
    [SerializeField] private List<string> _listOfObjectives;
    [SerializeField] private List<TextMeshProUGUI> _list;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _objective;
    [SerializeField] private string _objective2;

    //[SerializeField] private GameObject _objectivesListManager;
    [SerializeField] private Transform _textParent;
    [SerializeField] private Transform _textPrefab;
    [SerializeField] private GameObject _spawnedInPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _objective = "First mission is to make sure this string prints properly";
        _objective2 = "Second mission is to make sure first string gets deleted";
        AddToList(_objective);
        AddToList(_objective2);
        RemoveFromList(_objective);
        ShowList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(string Objective)
    {
        if (_textParent.transform.childCount == (_list.Count + 2))
        {

        }
        else
        {
            if (_textParent.transform.childCount < (_list.Count + 2))
            {
                _spawnedInPrefab = Instantiate(_textPrefab, new Vector3(0, 0, 0), Quaternion.identity, _textParent).gameObject;
                _text = _spawnedInPrefab.GetComponent<TextMeshProUGUI>();
                _text.text = Objective;
                _list.Add(_text);
            }
        }
    }

    public void RemoveFromList(string CompletedObjective)
    {
        TextMeshProUGUI text = new GameObject().AddComponent<TextMeshProUGUI>();
        text.text = CompletedObjective;
        Debug.Log("text: " + text.text);
        foreach(var item in _list.ToList())
        {
            if(item.text == CompletedObjective)
            {
                Destroy(item.gameObject);
                _list.Remove(item);
            }
        }
    }

    public void ShowList()
    {
        _textParent.gameObject.SetActive(true);
    }

    public void HideList()
    {
        _textParent.gameObject.SetActive(false);
    }

}
