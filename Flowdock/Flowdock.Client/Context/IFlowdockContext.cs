using Flowdock.Client.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowdock.Client.Context {
	public interface IFlowdockContext
	{
		Task<string> Login(string username, string password);

		Task<IEnumerable<Flow>> GetCurrentFlows();
		Task<Flow> GetFlow(string id);
		Task<IEnumerable<Message>> GetMessagesForFlow(string id);
        
        // TODO: This is a hack, find out a way to get all messages of a thread in one task call.
        Task<IEnumerable<Message>> GetFirstMessageForThread(string flowId, int threadId);
        Task<IEnumerable<Message>> GetRestOfMessagesForThread(string flowId, int threadId);

		void SendMessage(string flowId, string message);
	}
	
}
