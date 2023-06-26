using System.Text;
using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;

namespace CookiesBot.Gameplay
{
    public sealed class ShowInventoryBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IInventory _inventory;

        public ShowInventoryBot(ITelegram telegram, IInventory inventory)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;
        
        public bool CanGetUpdate(IUpdateInfo updateInfo)
            => updateInfo.Message!.Text != null && updateInfo.Message.Text.IsCommand("/showInventory");
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new ArgumentNullException(nameof(updateInfo));

            if (_inventory.Cells.Count != 0)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("Ваши предметы:\n\n");

                for (var i = 0; i < _inventory.Cells.Count; i++)
                    stringBuilder.Append($"{i + 1}. {_inventory.Cells[i].Item.Name}\nКоличество: {_inventory.Cells[i].Count}\nОписание: {_inventory.Cells[i].Item.Description}\n");

                _telegram.SendMessage(stringBuilder.ToString(), updateInfo.Message!.From!.Id);
                return;
            }

            _telegram.SendMessage("У вас нет предметов", updateInfo.Message!.From!.Id);
        }
    }
}