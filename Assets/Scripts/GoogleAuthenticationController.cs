using Core.FirebaseFeatures;
using UnityEngine;

public class GoogleAuthenticationController : MonoBehaviour
{

    public void SignIn()
    {
        GoogleAuthenticationService.Instance.SignInUser();
    }
    
    public void SignOut()
    {
        GoogleAuthenticationService.Instance.SignOut();
    }
}
