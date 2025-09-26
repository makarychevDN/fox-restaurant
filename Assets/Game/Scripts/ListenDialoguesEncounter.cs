using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class ListenDialoguesEncounter : Encounter
    {
        [SerializeField] private Character wolf;

        public override async Task StartEncounter()
        {
            await wolf.Say("Привет!<pause:0.4> Я крутой волчара.<pause:0.4> Накормишь меня <color:#bf623f>колбаской?");
            await wolf.Say("Круто!!!");
            await wolf.Say("Теперь мы друзья!");
        }
    }
}