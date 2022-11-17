using authen;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Uno.Extensions;
using Uno.Extensions.Authentication;
using Uno.Extensions.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace authen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();
        }

        private static IHost BuildAppHost()
        {
            return UnoHost
                    .CreateDefaultBuilder()
                    .UseAuthentication(auth =>
                                        auth.AddCustom(custom =>
                                        custom.Login(
                                                     async (sp, dispatcher, tokenCache, credentials, cancellationToken) =>
                                                     {
                                                         var isValid = credentials.TryGetValue("Username", out var username) && username == "Bob";
                                                         if (isValid)
                                                         {
                                                             credentials;
                                                         }
                                                         return null;
                                                     })
                                                    )
                );
        }

    }

    public ShellViewModel
{
    public async Task Start()
    {
        if (await _auth.RefreshAsync(CancellationToken.None))
        {
            await Navigator.NavigateViewModelAsync<SecondViewModel>(this);
        }
        else
        {
            await Navigator.NavigateViewModelAsync<MainViewModel>(this);
        }
    }
}
public MainViewModel
{
     public string? Username { get; set; }

private readonly IAuthenticationService _auth;
private readonly IDispatcher _dispatcher;
private readonly INavigator _navigator;

public MainViewModel(IDispatcher dispatcher, INavigator navigator, IAuthenticationService auth)
{
    _auth = auth;
    _dispatcher = dispatcher;
    _navigator = navigator;
}

public async Task Authenticate()
{
    if (await _auth.LoginAsync(_dispatcher, new Dictionary<string, string> { { "Username", Username ?? string.Empty } }, CancellationToken.None))
    {
        await _navigator.NavigateViewModelAsync<SecondViewModel>(this);
    }
}
}

public SecondViewModel
{
    public record SecondViewModel(IDispatcher Dispatcher, IAuthenticationService Auth)
    {
        public async Task Logout()
        {
            await Auth.LogoutAsync(Dispatcher, CancellationToken.None);
        }
    }
}
}


