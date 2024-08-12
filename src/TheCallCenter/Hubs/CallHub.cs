using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TheCallCenter.Data.Entities;

namespace TheCallCenter.Hubs
{
    public class CallHub : Hub<ICallHub>
    {
        public async Task CallAnswered(Call call)
        {
            await Clients.Others.CallAnswered(call);
        }

        public async Task NewCall(Call newCall)
        {
            await Clients.Others.NewCall(newCall);
        }
    }
}
