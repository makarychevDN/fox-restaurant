using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class ListenDialoguesEncounter : Encounter
    {
        [SerializeField] private Character wolf;

        public override async Task StartEncounter()
        {
            await wolf.Say("������!<pause:0.4> � ������ �������.<pause:0.4> ��������� ���� ���������?");
            await wolf.Say("�����!!!");
            await wolf.Say("������ �� ������!");
        }
    }
}