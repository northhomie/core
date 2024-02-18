using Core.FirebaseFeatures;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailAuthenticationController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private Button _signUpButton;

    private void Start()
    {
        _signUpButton.interactable = false;
        
        _email.onValueChanged.AddListener(OnEmailValueChanged);
        _password.onValueChanged.AddListener(OnPasswordValueChanged);
    }

    private void OnDestroy()
    {
        _email.onValueChanged.RemoveAllListeners();
        _password.onValueChanged.RemoveAllListeners();
    }

    public void SignUp()
    {
        EmailAuthenticationService.Instance.SignUpUser(_email.text, _password.text);
    }

    public void SignIn()
    {
        EmailAuthenticationService.Instance.SignInUser(_email.text, _password.text);
    }

    private void OnEmailValueChanged(string value)
    {
        
    }

    private void OnPasswordValueChanged(string value)
    {
        _signUpButton.interactable = true;
    }
}
