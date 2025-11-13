using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MelonLoader;
using HarmonyLib;
using Il2CppMonomiPark.KFC.FirstPerson.SpringEffect;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SR2Interpolation
{
    public class InterpolatorMod : MelonMod
    {
        private static MelonLogger.Instance? _logger;

        public override void OnInitializeMelon()
        {
            _logger = LoggerInstance;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName != "PlayerCore")
                return;

            var objs = Object.FindObjectsOfType<ObjectSpringEffect>();
            foreach (var spring in objs)
            {
                // the first person bobbing is controlled by a script which updates (I assume in FixedUpdate) the setpoint for a spring
                // but by default the _followingSharpness is 10000 which is way too high and causes it to snap each update
                spring._followingSharpness = 50f;
                _logger?.Msg($"Adjusted ObjectSpringEffect._followingSharpness {sceneName}");
            }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [HarmonyPatch(typeof(Object))]
        private class InstantiatePatch
        {
            private static IEnumerable<MethodBase> TargetMethods()
            {
                return typeof(Object).GetMethods()
                    .Where(method => method is { Name: "Instantiate", ContainsGenericParameters: false });
            }

            // ReSharper disable once InconsistentNaming
            private static void Postfix(Object __result)
            {
                // _logger?.Msg($"Instantiate {__result} {__result.name}");
                var go = __result.TryCast<GameObject>();
                if (go)
                {
                    HandleGameObject(go!);
                }
                else
                {
                    var cmp = __result.TryCast<Component>();
                    if (cmp)
                    {
                        HandleGameObject(cmp!.gameObject);
                    }
                }
            }

            private static void HandleGameObject(GameObject go)
            {
                // var count = 0;
                foreach (var rb in go.GetComponentsInChildren<Rigidbody>(true))
                {
                    if (!rb.isKinematic && rb.interpolation != RigidbodyInterpolation.Interpolate)
                    {
                        // seems like they forgot to turn on interpolation for everything, which is why all physics objects look low fps
                        rb.interpolation = RigidbodyInterpolation.Interpolate;
                        // count++;
                    }
                }
                // if (count > 0)
                //     _logger?.Msg($"Added interpolation to {count} Rigidbodies");
            }
        }
    }
}
