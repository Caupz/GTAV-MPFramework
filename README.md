# Instructions #

This framework is meant for Grand Theft Auto: V multiplayer platforms. Currently it is made for FiveM platform, but can be easily port to whatever GTA:V platform by changing some namespaces and classes. This resource only works with C# based gamemodes.  
     
Framework has about 100 callbacks in client- and serverside which can be used to make your main gamemode more stable and structured.  
  
This project is currently work in progress and if you have any suggestions then be welcome to let me know.

## Usage / Installation ##

1. Download MPFrameworkClient.dll and MPFrameworkServer.dll  
2. In your client resource add *PreBuiltDLLs/MPFrameworkClient.dll* as reference in Visual Studio  
3. In your server resource add *PreBuiltDLLs/MPFrameworkServer.dll* as reference in Visual Studio  
![Instructions to how to add dlls as reference](instruction-add-reference.png)  
4. In your client resource's Main class add using
```csharp
using MPFrameworkClient;

namespace CopsAndRobbers
{
    public class Main : BaseScript
    {
        ClientCore core = new ClientCore();

        public Main()
        {
            core.OnPlayerSpawned += OnPlayerSpawned;
            /* ... */
        }

        public void OnPlayerSpawned(int newPedHandle, int newPedNetworkId, float x, float y, float z)
        {
            Debug.WriteLine("CNR: OnPlayerSpawned NEW PED ID " + ClientCore.PedHandle);
        }

        [Tick]
        async Task OnTickMain()
        {
            core.Process(); // Without this in Tick none of the events will fire!
        }
    }
}
```
5. In your server resource's Main class add using
```csharp
using MPFrameworkServer;

namespace CopsAndRobbersServer
{
    public class Main : BaseScript
    {
        public Main()
        {
            ServerCore.OnPlayerSpawned += OnPlayerSpawned;
            /* ... Your code here ... */
        }

        public void OnPlayerSpawned(Player client)
        {
            Debug.WriteLine("CNR OnPlayerSpawned " + client.Handle);
            /* ... Your code here ... */
        }
    }
}
```
6. Build & run your gameserver.

### Contribution guidelines ###

* Suggestions for changes and further developments
* Code review

## NB: This framework is not tested with OneSync! ##