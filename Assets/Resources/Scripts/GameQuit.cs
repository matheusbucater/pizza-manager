using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuit : MonoBehaviour
{
    private bool       _isOnQuitMenu;
    private GameObject _activeCanvas;

    [SerializeField] private GameObject _canvasPrefab;
    
    void Start() {
        _isOnQuitMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isOnQuitMenu) {
                ExitQuitMenu();
            } else {
                EnterQuitMenu();
            }
        }
    }

    public void ClickQuit() {
        Application.Quit();
    }
    public void ClickCancel() {
        ExitQuitMenu();
    }

    private void EnterQuitMenu() {
        _isOnQuitMenu = true;
        _activeCanvas = Instantiate(_canvasPrefab) as GameObject;

        Button _exit = GameObject.Find("Exit").GetComponent<Button>();
        Button _cancel = GameObject.Find("Cancel").GetComponent<Button>();

        _exit.onClick.AddListener(ClickQuit);
        _cancel.onClick.AddListener(ClickCancel);
    }
    private void ExitQuitMenu() {
        _isOnQuitMenu = false;
        Destroy(_activeCanvas);
    }

}
