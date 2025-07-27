using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolutions;

    private int _currentResolutionIndex = 0;
    

    private void Start()
    {
        _resolutions = Screen.resolutions;
        _filteredResolutions = new List<Resolution>();

        _resolutionDropdown.ClearOptions();

        for (int i = 0; i < _resolutions.Length; i++)
        {

            _filteredResolutions.Add(_resolutions[i]);

        }

        List<string> options = new List<string>();
        for(int i = 0; i < _filteredResolutions.Count; i++)
        {
            string resolutionOption = _filteredResolutions[i].width + "x" + _filteredResolutions[i].height;
            options.Add(resolutionOption);
            if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int reolutionIndex)
    {
        Resolution resolution = _filteredResolutions[reolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
