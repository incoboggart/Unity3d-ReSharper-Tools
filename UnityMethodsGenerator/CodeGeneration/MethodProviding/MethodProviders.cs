using System;
using System.Collections.Generic;

using ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding.Implementations;

namespace ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding
{
    internal static class MethodProviders
    {
        public static IList<IMethodProvider> MonoBehaviour;

        public static IList<IMethodProvider> EditorWindow;

        public static IList<IMethodProvider> Editor;

        static MethodProviders()
        {
            Initialize();
        }

        public static void Initialize()
        {
            var intType = typeof (Int32);
            var singleType = typeof (Single);
            var singleArrayType = typeof (Single[]);

            MonoBehaviour = new IMethodProvider[]
            {
                new MethodProvider("Awake"){DefaultComment = "Awake is called when the script instance is being loaded."},
                new MethodProvider("Start"){DefaultComment = "Start is called on the frame when a script is enabled just before any of the Update methods is called the first time."},
                new MethodProvider("Update"){DefaultComment = "Update is called every frame, if the MonoBehaviour is enabled."},
                new MethodProvider("LateUpdate"){DefaultComment = "LateUpdate is called every frame, if the Behaviour is enabled."},
                new MethodProvider("OnDestroy"){DefaultComment = "This function is called when the MonoBehaviour will be destroyed."},

                new MethodProvider("OnEnable"){DefaultComment = "This function is called when the object becomes enabled and active."},
                new MethodProvider("OnDisable"){DefaultComment = "This function is called when the behaviour becomes disabled or inactive."},
                
                new MethodProvider("OnApplicationFocus"){DefaultComment = "Sent to all game objects when the player gets or loses focus."},
                new MethodProvider("OnApplicationPause"){DefaultComment = "Sent to all game objects when the player pauses."},
                new MethodProvider("OnApplicationQuit"){DefaultComment = "Sent to all game objects before the application is quit."},

                new MethodProvider("FixedUpdate"){DefaultComment = "This function is called every fixed framerate frame, if the MonoBehaviour is enabled."},

                new MethodProvider("OnCollisionEnter", new TextParameterDescription("collision", "Collision")){DefaultComment = "OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider."},
                new MethodProvider("OnCollisionStay", new TextParameterDescription("collision", "Collision")),
                new MethodProvider("OnCollisionExit", new TextParameterDescription("collision", "Collision")),

                new MethodProvider("OnCollisionEnter2D", new TextParameterDescription("collision", "Collision2D")),
                new MethodProvider("OnCollisionStay2D", new TextParameterDescription("collision", "Collision2D")),
                new MethodProvider("OnCollisionExit2D", new TextParameterDescription("collision", "Collision2D")),

                new MethodProvider("OnTriggerEnter", new TextParameterDescription("other", "Collider")),
                new MethodProvider("OnTriggerExit", new TextParameterDescription("other", "Collider")),
                new MethodProvider("OnTriggerStay", new TextParameterDescription("other", "Collider")),

                new MethodProvider("OnTriggerEnter2D", new TextParameterDescription("other", "Collider2D")),
                new MethodProvider("OnTriggerExit2D", new TextParameterDescription("other", "Collider2D")),
                new MethodProvider("OnTriggerStay2D", new TextParameterDescription("other", "Collider2D")),

                new MethodProvider("OnParticleCollision", new TextParameterDescription("other", "GameObject")),

                new MethodProvider("OnLevelWasLoaded", new TextParameterDescription("level", "int", intType.FullName, intType.Name)),
                new MethodProvider("OnGUI"),
                new MethodProvider("OnDrawGizmos"),
                new MethodProvider("OnDrawGizmosSelected"),

                new MethodProvider("OnMouseDown"),
                new MethodProvider("OnMouseDrag"),
                new MethodProvider("OnMouseEnter"),
                new MethodProvider("OnMouseExit"),
                new MethodProvider("OnMouseOver"),
                new MethodProvider("OnMouseUp"),
                new MethodProvider("OnMouseUpAsButton"),

                new MethodProvider("OnAnimatorIK", new TextParameterDescription("layerIndex", "int", intType.Name, intType.FullName)),
                new MethodProvider("OnAnimatorMove"),
                
                new MethodProvider("OnAudioFilterRead", new TextParameterDescription("data", "float[]", singleArrayType.Name, singleArrayType.FullName), new TextParameterDescription("channels", "int", intType.Name, intType.FullName)),
                new MethodProvider("OnBecameInvisible"),
                new MethodProvider("OnBecameVisible"),
                
                new MethodProvider("OnPlayerConnected", new TextParameterDescription("player", "NetworkPlayer")),
                new MethodProvider("OnPlayerDisconnected", new TextParameterDescription("player", "NetworkPlayer")),

                new MethodProvider("OnControllerColliderHit", new TextParameterDescription("hit", "ControllerColliderHit")),
                new MethodProvider("OnJointBreak", new TextParameterDescription("breakForce", "float", singleType.Name, singleType.FullName)),
                
                new MethodProvider("OnNetworkInstantiate", new TextParameterDescription("info", "NetworkMessageInfo")),
                new MethodProvider("OnSerializeNetworkView", new TextParameterDescription("stream", "BitStream"), new TextParameterDescription("info", "NetworkMessageInfo")),

                new MethodProvider("OnPreRender"),
                new MethodProvider("OnPostRender"),
                new MethodProvider("OnPreCull"),
                new MethodProvider("OnRenderImage", new TextParameterDescription("src", "RenderTexture"), new TextParameterDescription("dest", "RenderTexture")),
                
                new MethodProvider("OnWillRenderObject"),
                new MethodProvider("OnRenderObject"),

                new MethodProvider("OnValidate"),
                new MethodProvider("Reset"),

                new MethodProvider("OnConnectedToServer"),
                new MethodProvider("OnDisconnectedFromServer", new TextParameterDescription("info", "NetworkDisconnection")),
                new MethodProvider("OnFailedToConnect", new TextParameterDescription("error", "NetworkConnectionError")),
                new MethodProvider("OnFailedToConnectToMasterServer", new TextParameterDescription("error", "NetworkConnectionError")),
                new MethodProvider("OnServerInitialized"),
                new MethodProvider("OnMasterServerEvent", new TextParameterDescription("msEvent", "MasterServerEvent")),
            };

            foreach (var methodProvider in MonoBehaviour)
            {
                methodProvider.Initialize();
            }

            EditorWindow = new IMethodProvider[]
            {
                new MethodProvider("OnDestroy"){DefaultComment = "OnDestroy is called when the EditorWindow is closed."},
                new MethodProvider("OnFocus"){DefaultComment = "Called when the window gets keyboard focus."}, 
                new MethodProvider("OnGUI"){DefaultComment = "Implement your own editor GUI here."}, 
                new MethodProvider("OnHierarchyChange"){DefaultComment = "Called whenever the scene hierarchy has changed."}, 
                new MethodProvider("OnInspectorUpdate"){DefaultComment = "OnInspectorUpdate is called at 10 frames per second to give the inspector a chance to update."}, 
                new MethodProvider("OnLostFocus"){DefaultComment = "Called when the window loses keyboard focus."}, 
                new MethodProvider("OnProjectChange"){DefaultComment = "Called whenever the project has changed."}, 
                new MethodProvider("OnSelectionChange"){DefaultComment = "Called whenever the selection has changed."}, 
                new MethodProvider("Update"){DefaultComment = "Called 100 times per second on all visible windows."}, 
           };

            foreach (var provider in EditorWindow)
            {
                provider.Initialize();
            }
        }
    }
}
