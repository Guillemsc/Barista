using Barista.Client.Contexts;
using UnityEngine;

namespace Barista.Client.Bootstraps
{
    public class MainBootstrap : MonoBehaviour
    {
        private readonly ContextLoadersRepository contextLoadersRepository = new ContextLoadersRepository();
    }
}