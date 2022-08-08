using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MRD
{
    public class StringPrinter : MonoBehaviour
    {
        [ContextMenu("RelicInstance")]
        private void RelicInstancePrint()
        {
            StringBuilder sb = new();
            foreach (Type type in Assembly.GetAssembly(typeof(Relic)).GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Relic))))
            {
                sb.AppendLine("\t\t\t{ typeof(" + type.Name + "), () => new " + type.Name + "() },");
            }
            Debug.Log(sb.ToString());
        }
    }
}
